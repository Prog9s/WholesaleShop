using System.ComponentModel.DataAnnotations.Schema;

namespace WholesaleShop.Models.Entities
{
    // Payment Type Enum
    public enum PaymentType
    {
        ToCustomer = 1,
        ToSupplier = 2
    }
    public class Payment
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime PaymentDate { get; set; }
        public PaymentType Type { get; set; }
        public decimal Amount { get; set; }

        // FKs
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public string? Notes { get; set; }
    }
}
