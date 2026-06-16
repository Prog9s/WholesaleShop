namespace WholesaleShop.DTOS
{
    public class ProductsDtos
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
