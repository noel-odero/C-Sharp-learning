using Confluent.Kafka;
using System.Text.Json;

namespace OrderFlow.Kafka;

public class KafkaProducer : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private const string BootstrapServers = "localhost:9092";

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = BootstrapServers,
            Acks = Acks.All,
            MessageSendMaxRetries = 3,
            EnableIdempotence = true
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, string key, T message)
    {
        var json = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = key,
            Value = json,                 
            Headers = new Headers
            {
                { "source", System.Text.Encoding.UTF8.GetBytes("OrderFlow") },
                { "eventType", System.Text.Encoding.UTF8.GetBytes(topic) }
            }
        };

        var result = await _producer.ProduceAsync(topic, kafkaMessage);

        Console.WriteLine($"[Kafka] Message delivered to {result.Topic} " +
                         $"partition [{result.Partition}] " +
                         $"offset {result.Offset}");
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}