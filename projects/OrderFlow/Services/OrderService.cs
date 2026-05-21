// publisher
using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Models;


namespace OrderFlow.Services;

public class OrderService: IOrderService

{

    private readonly List<Func<object, OrderEventArgs, Task>> _orderPlacedHandlers = new();
    private readonly List<Func<object, OrderEventArgs, Task>> _orderShippedHandlers = new();


    public void Subscribe(string eventName, Func<object, OrderEventArgs, Task> handler)
    {
        if (eventName == "OrderPlaced") _orderPlacedHandlers.Add(handler);
        if (eventName == "OrderShipped") _orderShippedHandlers.Add(handler);
    }
    // list pf orders
    private readonly List<Order> _orders = new();

     // all orders
    public IReadOnlyList<Order> GetOrders()
    {
        return _orders.AsReadOnly();
    }


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

            await OnOrderPlaced(order);
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
    public async Task ShipOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"Shipping order of id {order.Id}");
            await Task.Delay(2000, cancellationToken);

            order.Status = OrderStatus.Shipped;

            await OnOrderShipped(order);
            
        }
        catch(OperationCanceledException)
        {
            Console.WriteLine("Oder shipment was cancelled");
            
        }
        catch(Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            
        }
    }

   

    // helpers - raise the events
    protected virtual async Task OnOrderPlaced(Order order)
    {
        var args = new OrderEventArgs(order, "Order places successfully");
        await Task.WhenAll(_orderPlacedHandlers.Select(h=> h(this, args)));
    }

    protected virtual async Task OnOrderShipped(Order order)
    {
        var args = new OrderEventArgs(order, "Order shipped successfully");
        await Task.WhenAll(_orderShippedHandlers.Select(h => h(this, args)));
    }
}