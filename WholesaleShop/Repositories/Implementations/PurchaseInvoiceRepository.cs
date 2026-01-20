using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations

{
    public class PurchaseInvoiceRepository: GenericRepository<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public PurchaseInvoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseInvoice> GetPurchaseInvoices()
        {
            return _context.PurchaseInvoices
                          .Include(S => S.Supplier)
                          .ToList();
        }

    }
}
