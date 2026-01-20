using System.ComponentModel.DataAnnotations.Schema;

namespace WholesaleShop.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int StockQuantity { get; set; }

        //// صورة المنتج
        //[NotMapped]
        //public IFormFile? ImageFile { get; set; } // هذا لن يخزن، نستخدمه فقط لرفع الملف
        //public string? ImageUrl { get; set; } // هذا سيخزن في قاعدة البيانات (مثلاً: /images/inv1.jpg)
        public ICollection<SalesInvoiceItem>? SalesInvoiceItems { get; set; }

    }
}
