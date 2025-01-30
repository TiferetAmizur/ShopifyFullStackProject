using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShopify.Models;
using WebAppShopify.DTOs;

namespace WebAppShopify.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResposeDTO>> GetAllProductsAsync();
        Task<ProductResposeDTO> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductRequestDTO productDto);
        Task UpdateProductAsync(int productId, ProductRequestDTO productDto);
        Task DeleteProductAsync(int productId);
    }
}
