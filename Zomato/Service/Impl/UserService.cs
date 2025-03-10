using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public async Task<bool> existsById(long userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userId);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == email);

            if (user == null)
            {
                throw new UserNotFoundException($"User with email '{email}' not found.");
            }

            return user;
        }

        public async Task<User> getUserFromId(long userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userId);

            if (user == null)
            {
                throw new UserNotFoundException($"User with Id '{userId}' not found.");
            }

            return user;
        }

        public async Task<User> save(User user)
        {
           var savedUser = _context.User.Add(user).Entity;
           await _context.SaveChangesAsync();
            return await GetUserByEmail(savedUser.email);
        }
    }
}
