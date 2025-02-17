namespace Zomato.Exceptions.CustomExceptionHandler
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
