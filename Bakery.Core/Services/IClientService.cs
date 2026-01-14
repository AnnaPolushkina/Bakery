using Bakery.Core.Entities;

namespace Bakery.Core.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client> CreateAsync(string name, string email);
    Task DeactivateAsync(Guid clientId);
}
