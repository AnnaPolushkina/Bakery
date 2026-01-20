using Bakery.Core.Entities;

namespace Bakery.Core.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task DeactivateAsync(Guid productId);
    Task<bool> ExistsAsync(Guid productId);

}

