using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppShopify.Data;
using WebAppShopify.Models;
using WebAppShopify.DTOs;
using WebAppShopify.Interfaces;
using Serilog;

namespace WebAppShopify.Services
{
    /// <summary>
    /// Provides business logic for managing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// Logs the number of products retrieved at the information level.
        /// </summary>
        public async Task<List<ProductResposeDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.ProductDescription)
                    .ToListAsync();

                var productResposeDTOs = products.Select(p => new ProductResposeDTO
                {
                    ProductId = p.ProductId,
                    AddedTimestamp = p.AddedTimestamp,
                    ProductName = p.ProductDescription.ProductName,
                    InStock = p.InStock,
                    StockArrivalDate = p.StockArrivalDate,
                }).ToList();


                Log.Information("Successfully retrieved {Count} products from the database.", productResposeDTOs.Count);
                return productResposeDTOs;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to retrieve products. Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// Logs at the information level if the product is found, or at the warning level if not.
        /// </summary>
        public async Task<ProductResposeDTO> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductDescription)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product != null)
                {
                    var productResposeDTO = new ProductResposeDTO
                    {
                        ProductId = product.ProductId,
                        AddedTimestamp = product.AddedTimestamp,
                        ProductName = product.ProductDescription.ProductName,
                        InStock = product.InStock,
                        StockArrivalDate = product.StockArrivalDate
                    };

                    Log.Information("Successfully retrieved product with ID {ProductId}.", productId);
                    return productResposeDTO;
                }
                else
                {
                    Log.Warning("Product with ID {ProductId} not found.", productId);
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to retrieve product with ID {ProductId}. Error: {Message}", productId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new product to the database.
        /// Logs at the information level when the product is successfully added.
        /// </summary>
        public async Task AddProductAsync(ProductRequestDTO productDto)
        {
            try
            {
                var product = new Product
                {
                    AddedTimestamp = DateTime.UtcNow,
                    InStock = productDto.InStock,
                    StockArrivalDate = productDto.StockArrivalDate,
                    ProductDescription = new ProductDescription
                    {
                        ProductName = productDto.ProductName
                    }
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                Log.Information("Product '{ProductName}' added successfully to the database.", productDto.ProductName);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to add product '{ProductName}'. Error: {Message}", productDto.ProductName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// Logs at the information level if the product is successfully updated, or at the warning level if not found.
        /// </summary>
        public async Task UpdateProductAsync(int productId, ProductRequestDTO productDto)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductDescription)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product != null)
                {
                    product.InStock = productDto.InStock;
                    product.StockArrivalDate = productDto.StockArrivalDate;
                    product.ProductDescription.ProductName = productDto.ProductName;

                    await _context.SaveChangesAsync();
                    Log.Information("Product with ID {ProductId} updated successfully.", productId);
                }
                else
                {
                    Log.Warning("Product with ID {ProductId} not found. Update operation skipped.", productId);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to update product with ID {ProductId}. Error: {Message}", productId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a product from the database.
        /// Logs at the information level if the product is successfully deleted, or at the warning level if not found.
        /// </summary>
        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductDescription)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    Log.Information("Product with ID {ProductId} deleted successfully.", productId);
                }
                else
                {
                    Log.Warning("Product with ID {ProductId} not found. Delete operation skipped.", productId);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to delete product with ID {ProductId}. Error: {Message}", productId, ex.Message);
                throw;
            }
        }
    }
}
