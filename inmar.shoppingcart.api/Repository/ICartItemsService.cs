using inmar.shoppingcart.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Repository
{
    public interface ICartItemsService
    {
        Task<List<CartItem>> GetCartItemsAsync();
        Task<List<CartItem>> GetCartItemsAsync(int productId, int userId);
        Task<List<CartItem>> GetUserCartItemsAsync(int userId);
        Task<CartItem> GetCartItemAsync(int cartItemId);
        Task<CartItem> SaveCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
        Task<CartItem> DeleteCartItemAsync(CartItem cartItem);
    }
}
