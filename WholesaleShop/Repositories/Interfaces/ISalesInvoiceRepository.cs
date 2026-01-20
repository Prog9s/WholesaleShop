using WholesaleShop.Models.Entities;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface ISalesInvoiceRepository : IGenericRepository<SalesInvoice>
    {
        IEnumerable<SalesInvoice> GetSalesInvoices();
    }
}
