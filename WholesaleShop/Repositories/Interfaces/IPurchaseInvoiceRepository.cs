using WholesaleShop.Models.Entities;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface IPurchaseInvoiceRepository : IGenericRepository<PurchaseInvoice>
    {
        IEnumerable<PurchaseInvoice> GetPurchaseInvoices();
    }
}
