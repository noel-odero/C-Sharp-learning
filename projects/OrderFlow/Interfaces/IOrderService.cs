// publisher contract
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Interfaces;

public interface IOrderService
{
    void Subscribe(IOrderPlacedHandler handler);
    void Subscribe(IOrderShippedHandler handler);
    void Unsubscribe(IOrderPlacedHandler handler);
    void Unsubscribe(IOrderShippedHandler handler);
    Task PlaceOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task ShipOrderAsync(Order order, CancellationToken cancellationToken = default);
    IReadOnlyList<Order> GetOrders();
}