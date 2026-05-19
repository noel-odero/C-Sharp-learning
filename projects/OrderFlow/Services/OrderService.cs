// publisher
using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Models;


namespace OrderFlow.Services;

public class OrderService: IOrderService

{
    // public delegate void OrderPlacedHandler(object source, OrderEventArgs args);
    // public event OrderPlacedHandler OrderPlaced;
    public event EventHandler<OrderEventArgs> OrderPlaced;
    public event EventHandler<OrderEventArgs> OrderShipped;

    // list pf orders
    private readonly List<Order> _orders = new List<Order>();


    // Placing an order
    public async Task PlaceOrderAsync(Order order, CancellationToken cancellationToken=default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"\nProcessing order for {order.Customer.Name}...");
            await Task.Delay(2000, cancellationToken);

            order.Status  = OrderStatus.Confirmed;
            _orders.Add(order);

            OnOrderPlaced(order);
        }
        catch(OperationCanceledException)
        {
            Console.WriteLine("Order placement cancelled.");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }


    // Shipping an order
    
}