using WholesaleShop.Models.Entities;
namespace WholesaleShop.Services.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<SalesInvoice> GetAllSalesInvoices();
        SalesInvoice? GetSalesInvoiceByUid(string Uid);
        bool CreateSalesInvoice(SalesInvoice invoice);
        bool UpdateSalesInvoice(string Uid, SalesInvoice invoice);
        bool DeleteSales(string Uid);

        IEnumerable<PurchaseInvoice> GetAllPurchaseInvoices();
        PurchaseInvoice? GetPurchaseInvoiceByUid(string Uid);
        bool CreatePurchaseInvoice(PurchaseInvoice invoice);
        bool UpdatePurchaseInvoice(string Uid, PurchaseInvoice invoice);
        bool DeletePurchase(string Uid);
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Supplier> GetSuppliers();
        bool DeleteByUid(string Uid);

    }
}
