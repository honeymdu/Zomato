namespace Zomato.Service
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Zomato.Entity.Enum;
    using Zomato.Model;

    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
