using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inmar.shoppingcart.api.Context;
using inmar.shoppingcart.api.Models;
using inmar.shoppingcart.api.Repository;

namespace inmar.shoppingcart.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemsService _service;

        public CartItemsController(ICartItemsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            return await _service.GetCartItemsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _service.GetCartItemAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return cartItem;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IList<CartItem>>> GetUserCartItems(int userId)
        {
            var cartItems = await _service.GetUserCartItemsAsync(userId);

            if (cartItems == null)
            {
                return NotFound();
            }

            return cartItems;
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
        {
            var cart = await _service.SaveCartItemAsync(cartItem);

            return CreatedAtAction("GetCartItem", new { id = cartItem.CartItemId }, cart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(CartItem cartItem)
        {
            cartItem = await _service.DeleteCartItemAsync(cartItem);

            return Ok(cartItem);
        }
    }
}
