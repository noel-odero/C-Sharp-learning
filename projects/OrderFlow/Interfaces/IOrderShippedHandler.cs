using OrderFlow.Events;

namespace OrderFlow.Interfaces;

public interface IOrderShippedHandler
{
    Task OnOrderShippedAsync(object source, OrderEventArgs args);
}