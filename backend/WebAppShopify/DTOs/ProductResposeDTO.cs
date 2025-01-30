namespace WebAppShopify.DTOs
{
    public class ProductResposeDTO
    {
        public int ProductId { get; set; }
        public DateTime AddedTimestamp { get; set; }
        public string ProductName { get; set; }
        public bool InStock { get; set; }
        public DateTime? StockArrivalDate { get; set; }
    }
}
