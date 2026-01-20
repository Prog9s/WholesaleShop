using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;
namespace WholesaleShop.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _unitOfWork._PaymentRepository.GetPayments();
        }

        public IEnumerable<Payment> GetPayments()
        {
            return _unitOfWork._PaymentRepository.GetAll(
                p => p.Customer,
                p => p.Supplier
                );
        }


        public IEnumerable<Customer> GetCustomers()
        {

            return _unitOfWork._CustomerRepository.GetAll();
        }
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _unitOfWork._SupplierRepository.GetAll();
        }
        public Payment? GetByUid(string Uid)
        {
            return _unitOfWork._PaymentRepository.GetByUid(Uid);
        }

        public bool Create(Payment payment)
        {
            _unitOfWork._PaymentRepository.Add(payment);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(string Uid)
        {
            var payment = _unitOfWork._PaymentRepository.GetByUid(Uid);

            if (payment == null)
            {
                return false;
            }
            _unitOfWork._PaymentRepository.Delete(payment.Id);
            _unitOfWork.Save();
            return true;
        }

        
        public bool Update(string Uid, Payment payment)
        {
            var Payment = _unitOfWork._PaymentRepository.GetByUid(Uid);
            if (Payment == null)
            {
                return false;
            }

            _unitOfWork._PaymentRepository.Update(payment);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Payment> GetAllPaymentsWithInvoices()
        {
            return _unitOfWork._PaymentRepository.GetAll(
                p => p.Customer,
                p => p.Supplier
                );
        }
    }
}

