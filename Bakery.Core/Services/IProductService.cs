using Bakery.Core.Entities;

namespace Bakery.Core.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        void Deactivate(Guid productId);

    }
}

