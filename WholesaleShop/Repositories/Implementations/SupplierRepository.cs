using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            var Suppliers = _context.Suppliers.ToList();
            return Suppliers;
        }
    }

}
