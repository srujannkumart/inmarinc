using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Models
{
    public class CartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
