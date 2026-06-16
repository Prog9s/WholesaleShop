using WholesaleShop.Models.Entities;

namespace WholesaleShop.DTOS
{
        // يمثل عنصر واحد داخل الفاتورة
    public class SalesItemDto
    {
        public int Id { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    // يمثل الفاتورة كاملة مع عناصرها
    public class SalesInvoiceDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RaminingAmount { get; set; }
        public string? Notes { get; set; }
        // قائمة العناصر التابعة لهذه الفاتورة
        public List<SalesItemDto> Items { get; set; } = new List<SalesItemDto>();
    }

}



