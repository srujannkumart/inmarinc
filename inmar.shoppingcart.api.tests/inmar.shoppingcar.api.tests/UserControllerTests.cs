using inmar.shoppingcart.api.Controllers;
using inmar.shoppingcart.api.Models;
using inmar.shoppingcart.api.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace inmar.shoppingcar.api.tests
{
    public class UserControllerTests
    {
        Mock<IUserService> _mockUserServcie;
        UsersController _controller;
        public UserControllerTests()
        {
            _mockUserServcie = new Mock<IUserService>();

            var user = new User { Name = "Test", PhoneNumber = "1234568790", UserId = 1 };

            _mockUserServcie.Setup(x => x.GetUserAsync(It.IsAny<int>())).ReturnsAsync(user);
            _mockUserServcie.Setup(x => x.GetUsersAsync()).ReturnsAsync(new List<User> { user });
            _mockUserServcie.Setup(x => x.CreateNewUserAsync(It.IsAny<User>())).ReturnsAsync(user);
            _mockUserServcie.Setup(x => x.UpdateUserAsync(It.IsAny<int>(),It.IsAny<User>())).ReturnsAsync(user);
            _mockUserServcie.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).Verifiable();

            _controller = new UsersController(_mockUserServcie.Object);
        }

        [Fact]
        public async Task GetUsers_Valid()
        {
            var result = await _controller.GetUsers();
            var users = result.Value;
            Assert.NotNull(users);
            Assert.NotEmpty(users);
        }
        [Fact]
        public async Task GetUser_Valid()
        {
            var result = await _controller.GetUser(1);
            var user = result.Value;
            Assert.NotNull(user);
        }
        [Fact]
        public async Task PutUser_Valid()
        {
            var result = await _controller.PutUser(1, new User { Name = "Test", PhoneNumber = "1234568790", UserId = 1 });
            Assert.True(((NoContentResult)result).StatusCode == 204);
        }

        [Fact]
        public async Task PostUser_Valid()
        {
            var result = await _controller.PostUser(new User { Name = "Test", PhoneNumber = "1234568790", UserId = 1 });
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteUser_Valid()
        {
            var result = await _controller.DeleteUser(1);
            Assert.True(((NoContentResult)result).StatusCode == 204);
        }
    }
}
