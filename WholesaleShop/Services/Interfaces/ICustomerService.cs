using WholesaleShop.Models.Entities;

namespace WholesaleShop.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetAllCustomersWithInvoices();
        Customer? GetByUid(string Uid);
        bool Create(Customer cust);
        bool Update(string Uid, Customer cust);
        bool Delete(string Uid);
    }

}
