using Zomato.Dto;

namespace Zomato.Service
{
    public interface IAuthService
    {
        String[] login(String email, String password);

        UserDto SignUp(SignUpDto signupDto);

        String refreshToken(String refreshToken);
    }
}
