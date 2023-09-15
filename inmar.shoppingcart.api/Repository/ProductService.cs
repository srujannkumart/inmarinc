using inmar.shoppingcart.api.Context;
using inmar.shoppingcart.api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Repository
{
    public class ProductService : IProductService
    {
        ShoppingCartContext _context;
        public ProductService(ShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateNewProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    throw new Exception("Product Not Found");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new Exception("User Not found");
            }

            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(int productId, Product product)
        {
            try
            {
                if (productId != product.ProductId)
                {
                    throw new Exception("User Not found");
                }
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return product;
        }
    }
}
