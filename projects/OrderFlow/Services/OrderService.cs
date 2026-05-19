// publisher
using OrderFlow.Events;
using OrderFlow.Interfaces;
using OrderFlow.Models;


namespace OrderFlow.Services;

public class OrderService: IOrderService

{
    // public delegate void OrderPlacedHandler(object source, OrderEventArgs args);
    // public event OrderPlacedHandler OrderPlaced;
    public event EventHandler<OrderEventArgs> OrderPlaced;
    public event EventHandler<OrderEventArgs> OrderShipped;

    // list pf orders
    private readonly List<Order> _orders = new List<Order>();


    // Placing an order
}