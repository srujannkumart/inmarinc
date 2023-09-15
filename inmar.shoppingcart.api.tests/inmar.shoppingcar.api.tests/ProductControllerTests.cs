using inmar.shoppingcart.api.Controllers;
using inmar.shoppingcart.api.Models;
using inmar.shoppingcart.api.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace inmar.shoppingcar.api.tests
{
    public class ProductControllerTests
    {
        Mock<IProductService> _mockproductService;
        ProductsController _controller;
        public ProductControllerTests()
        {
            _mockproductService = new Mock<IProductService>();

            var product = new Product
            {
                Name = "Test",
                Price = 500,
                InStock = true,
                ProductId = 1
            };

            _mockproductService.Setup(x => x.GetProductsAsync()).ReturnsAsync(new List<Product> { product });
            _mockproductService.Setup(x => x.GetProductAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mockproductService.Setup(x => x.CreateNewProductAsync(It.IsAny<Product>())).ReturnsAsync(product);
            _mockproductService.Setup(x => x.UpdateProductAsync(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(product);
            _mockproductService.Setup(x => x.DeleteProductAsync(It.IsAny<int>())).Verifiable();

            _controller = new ProductsController(_mockproductService.Object);
        }

        [Fact]
        public async Task GetProducts_Valid()
        {
            var result = await _controller.GetProducts();
            var users = result.Value;
            Assert.NotNull(users);
            Assert.NotEmpty(users);
        }
        [Fact]
        public async Task GetProduct_Valid()
        {
            var result = await _controller.GetProduct(1);
            var user = result.Value;
            Assert.NotNull(user);
        }
        [Fact]
        public async Task PutProduct_Valid()
        {
            var result = await _controller.PutProduct(1, new Product { Name = "Test", InStock = true, Price = 500, ProductId = 1 });
            Assert.True(((NoContentResult)result).StatusCode == 204);
        }

        [Fact]
        public async Task PostProduct_Valid()
        {
            var result = await _controller.PostProduct(new Product { Name = "Test", InStock = true, Price = 500 });
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteProduct_Valid()
        {
            var result = await _controller.DeleteProduct(1);
            Assert.True(((NoContentResult)result).StatusCode == 204);
        }
    }
}
