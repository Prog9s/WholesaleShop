using Microsoft.EntityFrameworkCore;
using WholesaleShop.Models.Entities;

namespace WholesaleShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }
        // Define DbSet properties for each entity
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices  { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems  { get; set; }
        public DbSet<SalesInvoice> SalesInvoices  { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItems  { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
     

    }
}
