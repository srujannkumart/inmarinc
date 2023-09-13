using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
    }
}
