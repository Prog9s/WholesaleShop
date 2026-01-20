using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations
{
    public class SalesInvoiceRepository : GenericRepository<SalesInvoice>, ISalesInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public SalesInvoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<SalesInvoice> GetSalesInvoices()
        {
            return _context.SalesInvoices
                          .Include(C => C.Customer)
                          .ToList();
        }
    
    }
}
