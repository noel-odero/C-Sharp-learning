using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Kafka;
using OrderFlow.Services;

namespace OrderFlow.Setup;

public class KafkaSetup : IAsyncDisposable
{
    private readonly KafkaProducer _producer;
    private readonly List<KafkaConsumer> _consumers = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly List<Task> _consumerTasks = new();
    public IOrderService OrderService { get; }

    public KafkaSetup()
    {
        _producer = new KafkaProducer();
        OrderService = new OrderService(_producer);
    }

    public void RegisterConsumers(
        EmailService emailService,
        InventoryService inventoryService,
        ShippingService shippingService)
    {
        var emailPlaced = new KafkaConsumer("orderflow-email-group", _producer);
        var inventory = new KafkaConsumer("orderflow-inventory-group", _producer);
        var emailShipped = new KafkaConsumer("orderflow-email-shipped-group", _producer);
        var shipping = new KafkaConsumer("orderflow-shipping-group", _producer);

        _consumers.AddRange(new[] { emailPlaced, inventory, emailShipped, shipping });

        _consumerTasks.Add(Task.Run(() => emailPlaced.ConsumeAsync<OrderEventArgs>(
            "order-placed",
            async (key, args) => await emailService.OnOrderPlacedAsync(args),
            _cts.Token)));

        _consumerTasks.Add(Task.Run(() => inventory.ConsumeAsync<OrderEventArgs>(
            "order-placed",
            async (key, args) => await inventoryService.OnOrderPlacedAsync(args),
            _cts.Token)));

        _consumerTasks.Add(Task.Run(() => emailShipped.ConsumeAsync<OrderEventArgs>(
            "order-shipped",
            async (key, args) => await emailService.OnOrderShippedAsync(args),
            _cts.Token)));

        _consumerTasks.Add(Task.Run(() => shipping.ConsumeAsync<OrderEventArgs>(
            "order-shipped",
            async (key, args) => await shippingService.OnOrderShippedAsync(args),
            _cts.Token)));

        Console.WriteLine("[KafkaSetup] All consumers started.");
    }

    public async Task ShutdownAsync()
    {
        Console.WriteLine("[KafkaSetup] Shutting down consumers...");

        _cts.Cancel();

        try
        {
            await Task.WhenAll(_consumerTasks);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[KafkaSetup] Error during shutdown: {ex.Message}");
        }

        Console.WriteLine("[KafkaSetup] All consumers stopped.");
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var consumer in _consumers)
        {
            try
            {
                consumer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KafkaSetup] Consumer dispose error: {ex.Message}");
            }
        }

        try
        {
            _producer.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[KafkaSetup] Producer dispose error: {ex.Message}");
        }

        _cts.Dispose();
        await ValueTask.CompletedTask;
    }
}