namespace Zomato.Service
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Zomato.Entity;
    using Zomato.Entity.Enum;

    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
