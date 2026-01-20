using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Payment> GetPayments()
        {
            return _context.Payments
              .Include(C => C.Customer)
              .Include(S => S.Supplier)
              .ToList();
        }
    }
}
