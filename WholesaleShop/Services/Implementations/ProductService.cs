using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;
namespace WholesaleShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Product product)
        {
            _unitOfWork._ProductRepository.Add(product);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(string Uid)
        {
            var product = _unitOfWork._ProductRepository.GetByUid(Uid);
            if (product == null)
            {
                return false;
            }
            _unitOfWork._ProductRepository.Delete(product.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork._ProductRepository.GetAll();
        }

        public IEnumerable<Product> GetAllProductsWithInvoices()
        {
            return _unitOfWork._ProductRepository.GetAll();
        }

        public Product? GetByUid(string Uid)
        {
            return _unitOfWork._ProductRepository.GetByUid(Uid);
        }

        public bool Update(string Uid, Product product)
        {
            var existingProduct = _unitOfWork._ProductRepository.GetByUid(Uid);
            if (existingProduct == null)
            {
                return false;
            }
            _unitOfWork._ProductRepository.Update(product);
            _unitOfWork.Save();
            return true;
        }
    }
}
