using WholesaleShop.Models.Entities;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        IEnumerable<Payment> GetPayments();
    }
}
