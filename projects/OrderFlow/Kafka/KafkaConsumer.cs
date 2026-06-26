using Confluent.Kafka;
using OrderFlow.Models;
using System.Text.Json;

namespace OrderFlow.Kafka;

public class KafkaConsumer : IDisposable
{
    private readonly IConsumer<string, string> _consumer;
    private readonly KafkaProducer _dlqProducer;
    private readonly string _groupId;
    private readonly int _maxRetries = 3;
    private const string BootstrapServers = "localhost:9092";

    public KafkaConsumer(string groupId, KafkaProducer dlqProducer)
    {
        _groupId = groupId;
        _dlqProducer = dlqProducer;

        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    public async Task ConsumeAsync<T>(
        string topic,
        Func<string, T, Task> handler,
        CancellationToken cancellationToken)
    {
        _consumer.Subscribe(topic);

        Console.WriteLine($"[{_groupId}] Subscribed to topic: {topic}");

        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);

                    if (result?.Message?.Value == null) continue;

                    Console.WriteLine($"[{_groupId}] Message received from " +
                                     $"topic: {result.Topic} " +
                                     $"partition: [{result.Partition}] " +
                                     $"offset: {result.Offset}");

                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);

                    if (message == null) continue;

                     var success = await RetryAsync(
                        () => handler(result.Message.Key, message),
                        _maxRetries,
                        cancellationToken);

                    if (success)
                    {
                        _consumer.Commit(result);
                        Console.WriteLine($"[{_groupId}] Offset {result.Offset} committed");
                    }
                    else
                    {
                        await SendToDlqAsync(topic, result, _maxRetries);

                        _consumer.Commit(result);

                        Console.WriteLine($"[{_groupId}] Message at offset " +
                                         $"{result.Offset} sent to DLQ and skipped");
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"[{_groupId}] Consume error: {ex.Error.Reason}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{_groupId}] Unexpected error: {ex.Message}");
                }
            }
        }, cancellationToken);
    }

    private async Task<bool> RetryAsync(
    Func<Task> action,
    int maxRetries,
    CancellationToken cancellationToken)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                await action();
                return true;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_groupId}] Attempt {attempt}/{maxRetries} " +
                                $"failed: {ex.Message}");

                if (attempt < maxRetries)
                {
                    var backoff = TimeSpan.FromSeconds(
                        Math.Min(Math.Pow(2, attempt), 10));

                    Console.WriteLine($"[{_groupId}] Retrying in {backoff.TotalSeconds}s...");

                    await Task.Delay(backoff, cancellationToken);
                }
            }
        }

        return false;
    }

    private async Task SendToDlqAsync(
        string originalTopic,
        ConsumeResult<string, string> result,
        int retryCount)
    {
        var dlqTopic = $"{originalTopic}-dlq";

        var dlqMessage = new DlqMessage
        {
            OriginalTopic = originalTopic,
            OriginalPartition = result.Partition.Value,
            OriginalOffset = result.Offset.Value,
            FailedMessage = result.Message.Value,
            ConsumerGroup = _groupId,
            ErrorReason = $"Failed after {retryCount} retries",
            FailedAt = DateTimeOffset.UtcNow,
            RetryCount = retryCount
        };

        Console.WriteLine($"[{_groupId}] Sending failed message to {dlqTopic}...");
        await _dlqProducer.ProduceAsync(dlqTopic, result.Message.Key, dlqMessage);
    }

    public void Dispose()
{
    try
    {
        _consumer.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{_groupId}] Error during consumer close: {ex.Message}");
    }
    finally
    {
        _consumer.Dispose();
    }
}
}