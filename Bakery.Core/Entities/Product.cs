namespace Bakery.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    public Product(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (price <= 0)
            throw new ArgumentException("Product price must be greater than zero.", nameof(price));

        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Product is already deactivated.");

        IsActive = false;
    }

    public bool CanBeSold()
    {
        return IsActive;
    }

}
