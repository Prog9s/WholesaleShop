using WholesaleShop.Models.Entities;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetProducts();
    }
}
