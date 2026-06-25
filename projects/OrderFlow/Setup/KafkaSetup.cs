using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Kafka;
using OrderFlow.Services;

namespace OrderFlow.Setup;

public class KafkaSetup : IDisposable
{
    private readonly KafkaProducer _producer;
    private readonly List<KafkaConsumer> _consumers = new();
    private readonly CancellationTokenSource _cts = new();
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
        var emailPlaced = new KafkaConsumer("orderflow-email-group");
        var inventory = new KafkaConsumer("orderflow-inventory-group");
        var emailShipped = new KafkaConsumer("orderflow-email-shipped-group");
        var shipping = new KafkaConsumer("orderflow-shipping-group");

        _consumers.AddRange(new[] { emailPlaced, inventory, emailShipped, shipping });

        Task.Run(() => emailPlaced.ConsumeAsync<OrderEventArgs>(
            "order-placed",
            async (key, args) => await emailService.OnOrderPlacedAsync(args),
            _cts.Token));

        Task.Run(() => inventory.ConsumeAsync<OrderEventArgs>(
            "order-placed",
            async (key, args) => await inventoryService.OnOrderPlacedAsync(args),
            _cts.Token));

        Task.Run(() => emailShipped.ConsumeAsync<OrderEventArgs>(
            "order-shipped",
            async (key, args) => await emailService.OnOrderShippedAsync(args),
            _cts.Token));

        Task.Run(() => shipping.ConsumeAsync<OrderEventArgs>(
            "order-shipped",
            async (key, args) => await shippingService.OnOrderShippedAsync(args),
            _cts.Token));

        Console.WriteLine("[KafkaSetup] All consumers started.");
    }

    public void Shutdown()
    {
        Console.WriteLine("[KafkaSetup] Shutting down consumers...");
        _cts.Cancel();
    }

    public void Dispose()
    {
        _consumers.ForEach(c => c.Dispose());
        _producer.Dispose();
    }
}