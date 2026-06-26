using OrderFlow.Models;
using OrderFlow.Services;
using OrderFlow.Setup;

await using var kafka = new KafkaSetup(); 
var emailService = new EmailService();
var inventoryService = new InventoryService();
var shippingService = new ShippingService();

kafka.RegisterConsumers(emailService, inventoryService, shippingService);

var customer = new Customer(1, "Noel", "noel@alu.edu");

var menu = new MenuRunner(kafka.OrderService, customer);
await menu.RunAsync();

await kafka.ShutdownAsync();