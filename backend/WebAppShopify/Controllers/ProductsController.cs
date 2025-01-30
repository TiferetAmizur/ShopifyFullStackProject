using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShopify.Interfaces;
using WebAppShopify.Models;
using WebAppShopify.DTOs;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebAppShopify.Controllers
{
    /// <summary>
    /// Handles API requests related to product management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products with their descriptions.</returns>
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<ActionResult<List<ProductResposeDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching all products: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID, or 404 if not found.</returns>
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResposeDTO>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching product with ID {ProductId}: {Message}", id, ex.Message);
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">The product data transfer object.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductRequestDTO productDto)
        {
            try
            {
                await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductName }, productDto);
            }
            catch (Exception ex)
            {
                Log.Error("Error adding product: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The updated product data.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductRequestDTO productDto)
        {
            try
            {
                await _productService.UpdateProductAsync(id, productDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating product with ID {ProductId}: {Message}", id, ex.Message);
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Error deleting product with ID {ProductId}: {Message}", id, ex.Message);
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }
    }
}
