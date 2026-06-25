// publisher
using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Kafka;
using OrderFlow.Models;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly KafkaProducer _producer;
    private const string OrderPlacedTopic = "order-placed";
    private const string OrderShippedTopic = "order-shipped";
    private readonly List<Order> _orders = new();

    public OrderService(KafkaProducer producer)
    {
        _producer = producer;
    }

    public IReadOnlyList<Order> GetOrders() => _orders.AsReadOnly();

    public async Task PlaceOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"\nProcessing order for {order.Customer.Name}...");
            await Task.Delay(2000, cancellationToken);

            order.Status = OrderStatus.Confirmed;
            _orders.Add(order);

            var args = new OrderEventArgs(order, "Order placed successfully");
            await _producer.ProduceAsync(OrderPlacedTopic, order.Id.ToString(), args);

            Console.WriteLine($"Order #{order.Id} published to {OrderPlacedTopic}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Order placement cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public async Task ShipOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"\nShipping order #{order.Id}...");
            await Task.Delay(2000, cancellationToken);

            order.Status = OrderStatus.Shipped;

            var args = new OrderEventArgs(order, "Order shipped successfully");
            await _producer.ProduceAsync(OrderShippedTopic, order.Id.ToString(), args);

            Console.WriteLine($"Order #{order.Id} published to {OrderShippedTopic}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Order shipment was cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}