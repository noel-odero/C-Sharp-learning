using OrderFlow.Interfaces;
using OrderFlow.Models;

namespace OrderFlow.Setup;

public class MenuRunner
{
    private readonly IOrderService _orderService;
    private readonly Customer _customer;
    private int _orderCounter = 1;

    private readonly Dictionary<string, decimal> _catalogue = new()
    {
        { "T-Shirt", 19.99m },
        { "Sweater", 45.99m },
        { "Jeans", 59.99m },
        { "Jacket", 89.99m },
        { "Dress", 49.99m }
    };

    public MenuRunner(IOrderService orderService, Customer customer)
    {
        _orderService = orderService;
        _customer = customer;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n --- OrderFlow ---");
            Console.WriteLine("1. Place Order");
            Console.WriteLine("2. Ship Order");
            Console.WriteLine("3. View Orders");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await PlaceOrderAsync();
                    break;
                case "2":
                    await ShipOrderAsync();
                    break;
                case "3":
                    ViewOrders();
                    break;
                case "4":
                    Console.WriteLine("\nGoodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    private async Task PlaceOrderAsync()
    {
        Console.WriteLine("\n--- Available Products ---");
        foreach (var item in _catalogue)
        {
            Console.WriteLine($"{item.Key} — {item.Value:C}");
        }

        Console.Write("\nProduct name: ");
        var product = Console.ReadLine();

        if (string.IsNullOrEmpty(product) || !_catalogue.TryGetValue(product, out decimal price))
        {
            Console.WriteLine("Product not found. Please choose from the list.");
            return;
        }

        Console.Write("Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity. Enter a positive number.");
            return;
        }

        var order = new Order(_orderCounter++, _customer, product, quantity, price, OrderStatus.Pending);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await _orderService.PlaceOrderAsync(order, cts.Token);
    }

    private async Task ShipOrderAsync()
    {
        ViewOrders();

        Console.Write("\nEnter Order ID to ship: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var order = _orderService.GetOrders().FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            Console.WriteLine("Order not found.");
            return;
        }

        if (order.Status != OrderStatus.Confirmed)
        {
            Console.WriteLine($"Order #{orderId} cannot be shipped — current status is {order.Status}.");
            return;
        }

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await _orderService.ShipOrderAsync(order, cts.Token);
    }

    private void ViewOrders()
    {
        var orders = _orderService.GetOrders();

        if (orders.Count == 0)
        {
            Console.WriteLine("\nNo orders yet.");
            return;
        }

        Console.WriteLine("\n--- Orders ---");
        foreach (var order in orders)
        {
            Console.WriteLine(order);
        }
    }
}