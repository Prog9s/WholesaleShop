using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Repositories.Implementations;
using WholesaleShop.Repositories.Interfaces;
using WholesaleShop.UnitOfWork;

namespace WholesaleShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;   
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _CustomerRepository = new CustomerRepository(_context);
            _PaymentRepository = new PaymentRepository(_context);
            _ProductRepository = new ProductRepository(_context);
            _PurchaseInvoiceRepository = new PurchaseInvoiceRepository(_context);
            _SalesInvoiceRepository = new SalesInvoiceRepository(_context);
            _SupplierRepository = new SupplierRepository(_context);

        }
        public ICustomerRepository _CustomerRepository { get; }

        public IPaymentRepository _PaymentRepository { get; }

        public IProductRepository _ProductRepository { get; }

        public IPurchaseInvoiceRepository _PurchaseInvoiceRepository { get; }

        public ISalesInvoiceRepository _SalesInvoiceRepository { get; }

        public ISupplierRepository _SupplierRepository { get; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

