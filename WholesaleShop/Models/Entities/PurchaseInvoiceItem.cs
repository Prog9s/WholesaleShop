namespace WholesaleShop.Models.Entities
{
    public class PurchaseInvoiceItem
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Foreign Keys
        public int? PurchaseInvoiceId { get; set; }
        public PurchaseInvoice? PurchaseInvoice { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
