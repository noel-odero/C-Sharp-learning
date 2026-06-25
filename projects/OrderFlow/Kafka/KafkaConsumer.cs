using Confluent.Kafka;
using System.Text.Json;

namespace OrderFlow.Kafka;

public class KafkaConsumer : IDisposable
{
    private readonly IConsumer<string, string> _consumer;
    private readonly string _groupId;
    private bool _running = false;
    private const string BootstrapServers = "localhost:9092";

    public KafkaConsumer(string groupId)
    {
        _groupId = groupId;

        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = groupId,
            // Read from the beginning if no offset exists for this group
            AutoOffsetReset = AutoOffsetReset.Earliest,
            // We will commit offsets manually for control
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
        _running = true;

        Console.WriteLine($"[{_groupId}] Subscribed to topic: {topic}");

        await Task.Run(async () =>
        {
            while (_running && !cancellationToken.IsCancellationRequested)
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


                    await handler(result.Message.Key, message);

                    _consumer.Commit(result);

                    Console.WriteLine($"[{_groupId}] Offset {result.Offset} committed");
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

    public void Stop() => _running = false;

    public void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
    }
}