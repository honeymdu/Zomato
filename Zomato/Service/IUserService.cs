using System.Runtime.InteropServices;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IUserService
    {
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsersAsync();
        User getUserFromId(long userId);
        User save(User user);
        Boolean existsById(long userId);
    }
}
