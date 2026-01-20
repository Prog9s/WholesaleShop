using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;

namespace WholesaleShop.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Supplier supplier)
        {
            _unitOfWork._SupplierRepository.Add(supplier);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(string Uid)
        {
            var supplier = _unitOfWork._SupplierRepository.GetByUid(Uid);
            if (supplier == null)
            {
                return false;
            }
            _unitOfWork._SupplierRepository.Delete(supplier.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _unitOfWork._SupplierRepository.GetAll();
        }

        public IEnumerable<Supplier> GetAllSuppliersWithInvoices()
        {
            return _unitOfWork._SupplierRepository.GetAll();
        }

        public Supplier? GetByUid(string Uid)
        {
            return _unitOfWork._SupplierRepository.GetByUid(Uid);
        }

        public bool Update(string Uid, Supplier supplier)
        {
            var Supplier = _unitOfWork._SupplierRepository.GetByUid(Uid);
            if (Supplier == null)
            {
                return false;
            }
            _unitOfWork._SupplierRepository.Update(Supplier);
            _unitOfWork.Save();
            return true;
        }

    }

}

