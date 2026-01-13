using Bakery.Core;
using Bakery.Core.Entities;
using Bakery.Infrastructure.Persistence;

namespace Bakery.Infrastructure.Seed;

public static class ProductSeeder
{
    public static void Seed(BakeryDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new[]
        {
            new Product("Bread", 2.5m),
            new Product("Croissant", 3.2m),
            new Product("Cake", 15m)
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
