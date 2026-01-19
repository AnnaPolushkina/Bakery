namespace Bakery.Core.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(Guid clientId)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }

    public void AddItem(Guid productId, decimal price, int quantity)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Cannot modify order after creation.");

        _items.Add(new OrderItem(Id, productId, price, quantity));

    }

    public decimal GetTotalAmount()
    {
        return _items.Sum(i => i.GetTotal());
    }

    public void Confirm()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Order must contain at least one item.");

        Status = OrderStatus.Confirmed;
    }
}
