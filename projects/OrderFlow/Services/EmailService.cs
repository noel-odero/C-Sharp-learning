using OrderFlow.Events;
using OrderFlow.Interfaces;

namespace OrderFlow.Services;

public class EmailService : INotificationService
{
    public async Task OnOrderPlacedAsync(object source, OrderEventArgs args)
    {
        Console.WriteLine("Sending confirmation email...");
        await Task.Delay(1000);
        Console.WriteLine($"Email sent to {args.Order.Customer.Email}");
        Console.WriteLine($"Details {args.Order}");
    }
     public async Task OnOrderShippedAsync(object source, OrderEventArgs args)
    {
        Console.WriteLine("Sending shippinh notification");
        await Task.Delay(1000);
        Console.WriteLine($"Shipping email sent to {args.Order.Customer.Email}");
        Console.WriteLine($"Details {args.Order}");
    }
}