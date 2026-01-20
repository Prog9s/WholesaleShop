using System.ComponentModel.DataAnnotations.Schema;

namespace WholesaleShop.Models.Entities
{
    public class SalesInvoice
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // FK to Customer
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RaminingAmount { get; set; }

        // Invoice Reference (optional)
        //[NotMapped]
        //public IFormFile? ImageFile { get; set; } // هذا لن يخزن، نستخدمه فقط لرفع الملف
        //public string? ImageUrl { get; set; } // هذا سيخزن في قاعدة البيانات (مثلاً: /images/inv1.jpg)
        public string? Notes { get; set; }
        public ICollection<SalesInvoiceItem>? SalesInvoiceItems { get; set; }
    }
}
