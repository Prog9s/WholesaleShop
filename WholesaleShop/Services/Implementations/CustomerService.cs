using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;

namespace WholesaleShop.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Customer cust)
        {
            _unitOfWork._CustomerRepository.Add(cust);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(string Uid)
        { 
            var cust = _unitOfWork._CustomerRepository.GetByUid(Uid);
            if (cust == null)
            {
                return false;
            }
            _unitOfWork._CustomerRepository.Delete(cust.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _unitOfWork._CustomerRepository.GetAll();
        }

        public IEnumerable<Customer> GetAllCustomersWithInvoices()
        {
            return _unitOfWork._CustomerRepository.GetAll();
        }

        public Customer? GetByUid(string Uid)
        {
            return _unitOfWork._CustomerRepository.GetByUid(Uid);
        }

        public bool Update(string Uid, Customer cust)
        {
            var existingCust = _unitOfWork._CustomerRepository.GetByUid(Uid);
            if (existingCust == null)
            {
                return false;
            }
            _unitOfWork._CustomerRepository.Update(cust);
            _unitOfWork.Save();
            return true;
        }
    }
}
