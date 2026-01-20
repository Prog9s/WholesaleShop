using WholesaleShop.Models.Entities;
namespace WholesaleShop.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllProductsWithInvoices();
        Product? GetByUid(string Uid);
        bool Create(Product product);
        bool Update(string Uid, Product product);
        bool Delete(string Uid);
    }
}
