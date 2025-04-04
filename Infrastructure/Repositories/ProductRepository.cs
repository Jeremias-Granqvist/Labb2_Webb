using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    // Log or throw if no rows are affected
                    throw new Exception("Product not saved to the database.");
                }

                return product;
            }
            catch (Exception ex)
            {
                // Log the exception here or handle accordingly
                throw new Exception($"Error saving product: {ex.Message}");
            }
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                          .Select(p => new Product
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Description = p.Description,
                              Price = p.Price,
                              CategoryId = p.CategoryId,
                              Status = p.Status
                          })
                          .ToListAsync();

        }

        public Task<Product> UpdateProductAsync(int id, Product product)
        {
            var productToUpdate = _context.Products.FirstOrDefault(p => p.Id == id);

            if (productToUpdate == null)
            {
                return Task.FromResult<Product>(null);
            }
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            //productToUpdate.ProductCategory = product.ProductCategory;
            productToUpdate.Status= product.Status;
            productToUpdate.CategoryId = product.CategoryId;

            _context.SaveChangesAsync();
            return Task.FromResult(productToUpdate);
        }
    }
}
