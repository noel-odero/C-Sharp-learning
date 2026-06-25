// publisher contract
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Interfaces;

public interface IOrderService
{
    Task PlaceOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task ShipOrderAsync(Order order, CancellationToken cancellationToken = default);
    IReadOnlyList<Order> GetOrders();
}