using OrderFlow.Models;

namespace OrderFlow.Events;

public class OrderEventArgs : EventArgs // signal that this class carries the event data
{
    public Order Order {get; }
    public string Message {get; }
    public DateTimeOffset EventTime {get;}

    public OrderEventArgs(Order order, string message)
    {
        Order = order;
        Message = message;
        EventTime = DateTimeOffset.UtcNow;
    }

    public override string ToString()
    {
        return $"Order placed at: {EventTime}. {Message} -> {Order}";
    }
}