using WholesaleShop.Models.Entities;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier> 
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}
