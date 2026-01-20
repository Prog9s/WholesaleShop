using WholesaleShop.Data;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<WholesaleShop.Models.Entities.Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProducts()
        {
                return _context.Products.ToList();
        }
    }
}
