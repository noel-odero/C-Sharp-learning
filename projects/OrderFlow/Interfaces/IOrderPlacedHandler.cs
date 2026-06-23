// This is the subscriber contract
using OrderFlow.Events;

namespace OrderFlow.Interfaces;

public interface IOrderPlacedHandler
{
    Task OnOrderPlacedAsync(object source, OrderEventArgs args);
}
