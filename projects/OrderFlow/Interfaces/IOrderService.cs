// publisher contract
using OrderFlow.Events;

namespace OrderFlow.Interfaces;

public interface IOrderService
{
    event EventHandler<OrderEventArgs> OrderPlaced;
    event EventHandler<OrderEventArgs> OrderShipped;
}