namespace Bakery.WebApi.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool CanBeDeactivated { get; set; }
    }

}


