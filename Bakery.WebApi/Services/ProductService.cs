using Bakery.Core.Entities;
using Bakery.Core.Services;

namespace Bakery.WebApi.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>
            {
                new Product("Bread", 2.50m),
                new Product("Croissant", 1.80m),
                new Product("Cake", 15.00m)
            };

            return products.Where(p => p.CanBeSold());
        }

    }
}

