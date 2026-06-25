using OrderFlow.Events;
using OrderFlow.Interfaces;

namespace OrderFlow.Services;

public class InventoryService : IOrderPlacedHandler
{
    public async Task OnOrderPlacedAsync(OrderEventArgs args)
    {
        Console.WriteLine($"updating stock...");
        await Task.Delay(1000);
        Console.WriteLine($"Stock reduced by {args.Order.Quantity} for {args.Order.Product}");

    }

}
