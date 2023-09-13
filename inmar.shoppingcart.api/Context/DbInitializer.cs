using inmar.shoppingcart.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmar.shoppingcart.api.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ShoppingCartContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;   
            }

            var users = new User[]
            {
                new User{ Name="Caroline",PhoneNumber="1234567890" }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product{ Name = "Keyboard", Price = 5000, InStock = true }
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
