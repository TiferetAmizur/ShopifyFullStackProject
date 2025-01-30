namespace WebAppShopify.DTOs
{
    public class ProductRequestDTO
    {
        public string ProductName { get; set; }
        public bool InStock { get; set; }
        public DateTime? StockArrivalDate { get; set; }
    }
}
