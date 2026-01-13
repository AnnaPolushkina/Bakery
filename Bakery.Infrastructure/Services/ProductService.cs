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

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task DeactivateAsync(Guid productId)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
            throw new InvalidOperationException("Product not found.");

        product.Deactivate();

        await _context.SaveChangesAsync();
    }
}
