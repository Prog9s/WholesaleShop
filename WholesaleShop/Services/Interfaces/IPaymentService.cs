using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;
namespace WholesaleShop.Services.Interfaces
{
    public interface IPaymentService  
    {
        IEnumerable<Payment> GetAll();
        IEnumerable<Payment> GetPayments();
        Payment? GetByUid(string Uid);
        bool Create(Payment payment);
        bool Update(string Uid, Payment payment);
        bool Delete(string Uid);
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Supplier> GetSuppliers();

    }
}

//IGenericRepository<Payment>
