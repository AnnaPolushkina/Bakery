using Bakery.Core.Entities;
using Bakery.Core.Services;
using Bakery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly BakeryDbContext _context;

    public ClientService(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients
            .Where(c => c.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Client> CreateAsync(string name, string email)
    {
        var client = new Client(name, email);

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client;
    }

    public async Task DeactivateAsync(Guid clientId)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            throw new InvalidOperationException("Client not found.");

        client.Deactivate();

        await _context.SaveChangesAsync();
    }
}
