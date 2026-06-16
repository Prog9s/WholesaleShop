using WholesaleShop.Models.Entities;
namespace WholesaleShop.Services.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        IEnumerable<Supplier> GetAllSuppliersWithInvoices();
        Supplier? GetByUid(string Uid);
        bool Create(Supplier supplier);
        bool Update(string Uid, Supplier supplier);
        bool Delete(string Uid);
        bool DeleteByUid(string Uid);

    }
}
