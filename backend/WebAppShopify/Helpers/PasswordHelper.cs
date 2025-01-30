using BCrypt.Net;

namespace WebAppShopify.Helpers
{
    public static class PasswordHelper
    {
        // Method to generate hash for a plain text password
        public static string HashPassword(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        // Method to verify if a plain text password matches the hashed password
        public static bool VerifyPassword(string plainTextPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, storedHash);
        }
    }
}
