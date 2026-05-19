namespace OrderFlow.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered
}

public class Order
{
    public int Id{get; set;}
    public Customer Customer{get; set;}
    public string Product{get; set;}
    public int Quantity {get; set;}
    public decimal Price {get; set;}
    public OrderStatus Status{get; set;}
    public DateTimeOffset CreatedAt{get; set;}

    public Order(int id, Customer customer, string product, int quantity, decimal price, OrderStatus status)
    {
        Id = id;
        Customer = customer;
        Product = product;
        Quantity = quantity;
        Price = price;
        Status = OrderStatus.Pending;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public override string ToString()
    {
        return $"Order #{Id}: {Quantity}x {Product} for {Price:C} ({Status}) - placed on {CreatedAt:u}";
    }
}