using Zomato.Dto;
using Zomato.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Zomato.Entity;
using Zomato.Entity.Enum;
using System.Threading.Tasks;

namespace Zomato.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ITokenService tokenService;

        public AuthService(AppDbContext context, IConfiguration config, ITokenService tokenService)
        {
            _context = context;
            _config = config;
            this.tokenService = tokenService;
        }

        public async Task<string[]> login(string email, string password)
        {
            var user = _context.User.FirstOrDefault(u => u.email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var accessToken = GenerateJwtToken(user);
       
           await _context.SaveChangesAsync();

            return new string[] { accessToken };
        }

        private string GenerateJwtToken(User user)
        {
           return tokenService.GenerateToken(user);
        }

        public async Task<UserDto> SignUp(SignUpDto signupDto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(signupDto.password);

            var user = new User
            {
                name = signupDto.name,
                email = signupDto.email,
                password = hashedPassword,
                contact = signupDto.contact,
                role = Role.CONSUMER
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto { name = user.name, email = user.email, role = user.role ,contact = user.contact};


        }

        public string refreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }

}