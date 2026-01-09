using Bakery.Core;
using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Persistence;

public class BakeryDbContext : DbContext
{
    public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
