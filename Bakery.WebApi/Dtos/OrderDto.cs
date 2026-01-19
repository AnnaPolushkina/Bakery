namespace Bakery.WebApi.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public IEnumerable<OrderItemDto> Items { get; set; } = [];
}
