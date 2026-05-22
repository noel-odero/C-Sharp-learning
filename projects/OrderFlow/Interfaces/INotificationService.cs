// This is the subscriber contract

using OrderFlow.Events;

namespace OrderFlow.Interfaces;

public interface INotificationService
{
    Task OnOrderPlacedAsync(object source, OrderEventArgs args) => Task.CompletedTask;
    Task OnOrderShippedAsync(object source, OrderEventArgs args) => Task.CompletedTask;
}

// two Interfaces
// unsubscribe method