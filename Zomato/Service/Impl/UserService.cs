using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Expressions;
using Zomato.Data;
using Zomato.Entity;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool existsById(long userId)
        {
            var user = _context.User.FirstOrDefault(u => u.id == userId);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<User> GetAllUsersAsync()
        {
            return _context.User.ToList();
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.User.FirstOrDefault(u => u.email == email);

            if (user == null)
            {
                throw new UserNotFoundException($"User with email '{email}' not found.");
            }

            return user;
        }

        public User getUserFromId(long userId)
        {
            var user = _context.User.FirstOrDefault(u => u.id == userId);

            if (user == null)
            {
                throw new UserNotFoundException($"User with Id '{userId}' not found.");
            }

            return user;
        }

        public User save(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return GetUserByEmail(user.email);
        }
    }
}
