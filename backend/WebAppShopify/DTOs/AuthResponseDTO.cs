namespace WebAppShopify.DTOs
{
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public long IssuedAt {get; set;}
    }
}
