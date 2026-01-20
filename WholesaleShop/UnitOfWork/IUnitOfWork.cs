using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Implementations;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.UnitOfWork
{
    public interface IUnitOfWork
    {
        //IGenericRepository<Customer> _CustomerRepository { get; }
        //IGenericRepository<Payment> _PaymentRepository { get; }
        //IGenericRepository<Product> _ProductRepository { get; }
        //IGenericRepository<PurchaseInvoice> _PurchaseInvoiceRepository { get; }
        //IGenericRepository<SalesInvoice> _SalesInvoiceRepository { get; }
        //IGenericRepository<Supplier> _SupplierRepository { get; }
        ICustomerRepository _CustomerRepository { get; }
        IPaymentRepository _PaymentRepository { get; }
        IProductRepository _ProductRepository { get; }
        IPurchaseInvoiceRepository _PurchaseInvoiceRepository { get; }
        ISalesInvoiceRepository _SalesInvoiceRepository { get; }
        ISupplierRepository _SupplierRepository { get; }
        void Save();
    }
}
