using System.Runtime.InteropServices;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        IEnumerable<User> GetAllUsersAsync();
        Task<User> getUserFromId(long userId);
        Task<User> save(User user);
        Boolean existsById(long userId);
    }
}
