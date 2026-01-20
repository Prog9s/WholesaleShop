using System.ComponentModel.DataAnnotations.Schema;

namespace WholesaleShop.Models.Entities
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string InvoiceNumber { get; set; }
        // FK to Supplier
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RaminingAmount { get; set; }
        public string? Notes { get; set; }
        public ICollection<PurchaseInvoiceItem>? PurchaseInvoiceItems { get; set; }

    }
}
