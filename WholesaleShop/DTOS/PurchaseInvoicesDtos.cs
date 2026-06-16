using WholesaleShop.Models.Entities;

namespace WholesaleShop.DTOS
{
    public class PurchaseInvoicesDtos
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RaminingAmount { get; set; }
        public string? Notes { get; set; }
        public List<PurchaseItemDtos> Items { get; set; } = new List<PurchaseItemDtos>();

    }
    public class PurchaseItemDtos
    {
        public int Id { get; set; }
        //Foreign Keys
        public int? PurchaseInvoiceId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal LineTotal { get; set; }

    }

}
