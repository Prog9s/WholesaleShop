namespace WholesaleShop.Models.Entities
{
    public class Customer
    {
		public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
		public string Address { get; set; }
		public decimal CurrentBalance { get; set; }
        //FK to SalesInvoice and Payment
        public ICollection<SalesInvoice>? SalesInvoices { get; set; }
        public ICollection<Payment>? Payments { get; set; }

    }
}
