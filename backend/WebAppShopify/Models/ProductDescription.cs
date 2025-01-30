using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppShopify.Models
{
    public class ProductDescription
    {
        [Key]
        public int ProductId { get; set; } // Foreign key
        [Required]
        public string ProductName { get; set; }

        // Navigation property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
