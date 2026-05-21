using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Models;
using OrderFlow.Services;

// code against contract for loose coupling
IOrderService orderService = new OrderService();
INotificationService emailService = new EmailService();
INotificationService inventoryService = new InventoryService();
INotificationService shippingService = new ShippingService();

//  subcribe to the events
orderService.Subscribe("OrderPlaced", emailService.OnOrderPlacedAsync);
orderService.Subscribe("OrderPlaced", inventoryService.OnOrderPlacedAsync);
orderService.Subscribe("OrderShipped", emailService.OnOrderShippedAsync);
orderService.Subscribe("OrderShipped", shippingService.OnOrderShippedAsync);
//  customer 
var customer = new Customer(1, "Noel", "noel@alu.edu");
int orderCounter = 1;

//  Menu
await RunMenuAsync();

async Task RunMenuAsync()
{
    while (true)
    {
        Console.WriteLine("\n ---OrderFlow ---");
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

async Task PlaceOrderAsync()
{
    Console.Write("Product name: ");
    var product = Console.ReadLine();

    Console.Write("Quantity: ");
    if (!int.TryParse(Console.ReadLine(), out int quantity))
    {
        Console.WriteLine("Invalid quantity. Enter a number");
        return;
    }

    Console.Write("Price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("Invalid price. Enter a valid price");
        return;
    }

    var order = new Order(orderCounter++, customer, product, quantity, price, OrderStatus.Pending);

    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    await orderService.PlaceOrderAsync(order, cts.Token);
}

async Task ShipOrderAsync()
{
    ViewOrders();

    Console.Write("\nEnter Order ID to ship: ");
    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    var order = orderService.GetOrders().FirstOrDefault(o => o.Id == orderId);

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
    await orderService.ShipOrderAsync(order, cts.Token);
}

void ViewOrders()
{
    var orders = orderService.GetOrders();

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