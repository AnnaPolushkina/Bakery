using Bakery.Core.Entities;
using Bakery.Core.Services;

namespace Bakery.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product("Bread", 2.50m),
                new Product("Croissant", 1.80m),
                new Product("Cake", 15.00m)
            };
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Where(p => p.CanBeSold());
        }

        public void Deactivate(Guid productId)
        {
            var product = _products.SingleOrDefault(p => p.Id == productId);

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            product.Deactivate();
        }
    }
}


