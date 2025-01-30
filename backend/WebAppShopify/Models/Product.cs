using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAppShopify.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public DateTime AddedTimestamp { get; set; }
        [Required]
        public bool InStock { get; set; }
        public DateTime? StockArrivalDate { get; set; }

        // Navigation property
        [Required]
        public virtual ProductDescription ProductDescription { get; set; }
    }
}
