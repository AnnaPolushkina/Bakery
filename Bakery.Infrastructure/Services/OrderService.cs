using Serilog;
using Bakery.Core.Entities;
using Bakery.Core.Services;
using Bakery.Core.Exceptions;
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

    private async Task<Order> GetOrderOrThrow(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new NotFoundException("Order not found.");

        return order;
    }

    public async Task<Order> CreateAsync(Guid clientId)
    {
        Log.Information("Creating order for client {ClientId}", clientId);

        var order = new Order(clientId);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        Log.Information("Order {OrderId} created", order.Id);

        return order;
    }

    public async Task<Order> GetByIdAsync(Guid orderId)
    {
        return await GetOrderOrThrow(orderId);
    }

    public async Task AddItemAsync(
        Guid orderId,
        Guid productId,
        decimal price,
        int quantity)
    {
        var order = await GetOrderOrThrow(orderId);

        order.AddItem(productId, price, quantity);

        Log.Information("Adding item to order {OrderId}", orderId);

        await _context.SaveChangesAsync();
    }

    public async Task ConfirmAsync(Guid orderId)
    {
        var order = await GetOrderOrThrow(orderId);

        Log.Information("Confirming order {OrderId}", orderId);

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
