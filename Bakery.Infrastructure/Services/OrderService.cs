using Bakery.Core.Entities;
using Bakery.Core.Services;
using Bakery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly BakeryDbContext _context;

    public OrderService(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Guid clientId)
    {
        var order = new Order(clientId);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task AddItemAsync(
        Guid orderId,
        Guid productId,
        decimal price,
        int quantity)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new InvalidOperationException("Order not found.");

        order.AddItem(productId, price, quantity);

        await _context.SaveChangesAsync();
    }

    public async Task ConfirmAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new InvalidOperationException("Order not found.");

        order.Confirm();

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetByClientAsync(Guid clientId)
    {
        return await _context.Orders
            .Where(o => o.ClientId == clientId)
            .Include(o => o.Items)
            .AsNoTracking()
            .ToListAsync();
    }
}
