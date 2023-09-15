using inmar.shoppingcart.api.Context;
using inmar.shoppingcart.api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Repository
{
    public class CartItemsService : ICartItemsService
    {
        ShoppingCartContext _context;
        public CartItemsService(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem == null)
                {
                    throw new Exception("Cart Item Not Found");
                }
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CartItem> DeleteCartItemAsync(CartItem cartItem)
        {
            try
            {
                var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId && x.UserId == cartItem.UserId);
                if (existingCartItem == null)
                {
                    throw new Exception("Cart Item Not Found");
                }
                else
                {
                    if (existingCartItem.Quantity > 0)
                        existingCartItem.Quantity -= cartItem.Quantity;
                    _context.CartItems.Update(existingCartItem);
                }

                await _context.SaveChangesAsync();

                return existingCartItem ?? cartItem;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CartItem> GetCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                throw new Exception("Cart Item Not found");
            }

            return cartItem;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public Task<List<CartItem>> GetCartItemsAsync(int productId, int userId)
        {
            return _context.CartItems.Where(x => x.ProductId == productId && x.UserId == userId).ToListAsync();
        }

        public Task<List<CartItem>> GetUserCartItemsAsync(int userId)
        {
            return _context.CartItems.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<CartItem> SaveCartItemAsync(CartItem cartItem)
        {            
            try
            {
                if (cartItem.Quantity == 0) cartItem.Quantity = 1;
                if (cartItem.UserId == 0 || cartItem.ProductId == 0) throw new Exception("User Id or Product Id can't be zero");

                if (_context.CartItems.Any(x => x.ProductId == cartItem.ProductId && x.UserId == cartItem.UserId))
                {
                    var existingItem = _context.CartItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.UserId == cartItem.UserId);
                    existingItem.Quantity += cartItem.Quantity;

                    _context.CartItems.Update(existingItem);
                    cartItem = existingItem;
                }
                else
                {
                    _context.CartItems.Add(cartItem);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return cartItem;
        }
    }
}
