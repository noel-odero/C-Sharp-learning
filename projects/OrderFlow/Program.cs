using OrderFlow.Models;
using OrderFlow.Services;
using OrderFlow.Setup;

// Kafka setup
using var kafka = new KafkaSetup();

// Services
var emailService = new EmailService();
var inventoryService = new InventoryService();
var shippingService = new ShippingService();

// Register consumers
kafka.RegisterConsumers(emailService, inventoryService, shippingService);

// Customer
var customer = new Customer(1, "Noel", "noel@alu.edu");

// Run menu
var menu = new MenuRunner(kafka.OrderService, customer);
await menu.RunAsync();

// Shutdown
kafka.Shutdown();