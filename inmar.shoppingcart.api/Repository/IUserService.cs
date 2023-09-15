using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmar.shoppingcart.api.Models;

namespace inmar.shoppingcart.api.Repository
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task<User> CreateNewUserAsync(User user);
        Task<User> UpdateUserAsync(int userId, User user);
        Task DeleteUserAsync(int userId);
    }
}
