using Microsoft.AspNetCore.Authentication.OAuth;
using System.Threading.Tasks;
using WebAppShopify.DTOs;

namespace WebAppShopify.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> AuthenticateAsync(string username, string password);
    }
}
