namespace Bakery.WebApi.Dtos;

public class AddOrderItemRequest
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
