using Bakery.Core;
using Bakery.Core.Entities;
using Bakery.Core.Services;
using Bakery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly BakeryDbContext _context;

    public ProductService(BakeryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .Where(p => p.IsActive)
            .AsNoTracking()
            .ToList();
    }

    public void Deactivate(Guid productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
            throw new InvalidOperationException("Product not found.");

        product.Deactivate();

        _context.SaveChanges();
    }
}



