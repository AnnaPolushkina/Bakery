namespace Bakery.Core.Entities;

public class OrderItem
{
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Guid productId, decimal price, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public decimal GetTotal()
    {
        return Price * Quantity;
    }
}
