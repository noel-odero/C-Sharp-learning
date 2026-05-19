// This is the subscriber contract

namespace OrderFlow.Interfaces;

public interface INotificationService
{
    Task OnOrderPlacedAsync(object source, OrderEventArgs args);
    Task OnOrderShippedAsync(object source, OrderEventArgs args);
}