// publisher
using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Models;


namespace OrderFlow.Services;

public class OrderService: IOrderService

{

    private readonly List<IOrderPlacedHandler> _orderPlacedHandlers = new();
    private readonly List<IOrderShippedHandler> _orderShippedHandlers = new();

    public void Subscribe(IOrderPlacedHandler handler) => _orderPlacedHandlers.Add(handler);
    public void Subscribe(IOrderShippedHandler handler) => _orderShippedHandlers.Add(handler);
    public void Unsubscribe(IOrderPlacedHandler handler) => _orderPlacedHandlers.Remove(handler);
    public void Unsubscribe(IOrderShippedHandler handler) => _orderShippedHandlers.Remove(handler);
    // list of  orders
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

            await OnOrderShippedAsync(order);
            
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

   

    // helpers - raise the events - loop though the subscribers and start them in parallel.
    protected virtual async Task OnOrderPlaced(Order order)
    {
        var args = new OrderEventArgs(order, "Order places successfully");
        await Task.WhenAll(_orderPlacedHandlers.Select(h => h.OnOrderPlacedAsync(this, args)));
    }

    protected virtual async Task OnOrderShippedAsync(Order order)
    {
        var args = new OrderEventArgs(order, "Order shipped successfully");
        await Task.WhenAll(_orderShippedHandlers.Select(h => h.OnOrderShippedAsync(this, args)));
    }
}