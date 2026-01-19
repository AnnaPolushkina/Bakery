using Bakery.Core.Entities;

namespace Bakery.Core.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(Guid clientId);
    Task<Order?> GetByIdAsync(Guid orderId);


    Task AddItemAsync(
        Guid orderId,
        Guid productId,
        decimal price,
        int quantity);

    Task ConfirmAsync(Guid orderId);

    Task<IEnumerable<Order>> GetByClientAsync(Guid clientId);
}

