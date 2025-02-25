using Zomato.Dto;

namespace Zomato.Service
{
    public interface IAuthService
    {
        Task<String[]> login(String email, String password);

        Task<UserDto> SignUp(SignUpDto signupDto);

        String refreshToken(String refreshToken);
    }
}
