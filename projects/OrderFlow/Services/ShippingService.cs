// Services/ShippingService.cs
using OrderFlow.Events;
using OrderFlow.Interfaces;

namespace OrderFlow.Services
{
    public class ShippingService : INotificationService
    {
        public async Task OnOrderShippedAsync(object source, OrderEventArgs args)
        {
            Console.WriteLine($"\n Creating shipment...");
            await Task.Delay(1000);
            Console.WriteLine($"Shipment created for order {args.Order.Id}");
            Console.WriteLine($"Delivering to {args.Order.Customer.Name}");
        }

        public async Task OnOrderPlacedAsync(object source, OrderEventArgs args)
        {
            await Task.CompletedTask;
        }
    }
}