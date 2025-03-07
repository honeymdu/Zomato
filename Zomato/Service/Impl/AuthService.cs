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
using AutoMapper;
using System.Collections.Generic;

namespace Zomato.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ITokenService tokenService;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, IConfiguration config, ITokenService tokenService,IMapper mapper)
        {
            _context = context;
            _config = config;
            this.tokenService = tokenService;
            this._mapper = mapper;

        }

        public async Task<string[]> login(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var accessToken = GenerateJwtToken(user);
      
            return new string[] { accessToken };
        }

        private string GenerateJwtToken(User user)
        {
           return tokenService.GenerateToken(user);
        }

        public async Task<UserDto> SignUp(SignUpDto signupDto)
        {
            if (signupDto == null)
            {
                throw new ArgumentNullException(nameof(signupDto), "Signup data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(signupDto.password))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(signupDto.password));
            }

            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.email == signupDto.email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(signupDto.password);
            var address = _mapper.Map<Address>(signupDto.addresses);
            List<Address> addresses = new List<Address>();
            addresses.Add(address);
            var user = new User
            {
                name = signupDto.name,
                email = signupDto.email,
                password = hashedPassword,
                contact = signupDto.contact,
                role = Role.CONSUMER,
                addresses = addresses
            };

            _context.User.Add(user);
            int savedRows = await _context.SaveChangesAsync();

            if (savedRows == 0)
            {
                throw new Exception("User registration failed. No records were saved.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public string refreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }

}