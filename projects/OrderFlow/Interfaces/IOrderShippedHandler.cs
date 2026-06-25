using OrderFlow.Events;

namespace OrderFlow.Interfaces;

public interface IOrderShippedHandler
{
    Task OnOrderShippedAsync(OrderEventArgs args);
}