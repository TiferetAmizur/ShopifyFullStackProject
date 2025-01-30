using System.ComponentModel.DataAnnotations;

namespace WebAppShopify.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }  // e.g., "Admin", "Viewer"
    }
}
