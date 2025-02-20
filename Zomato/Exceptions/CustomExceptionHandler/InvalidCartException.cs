namespace Zomato.Exceptions.CustomExceptionHandler
{
    public class InvalidCartException: Exception
    {
        public InvalidCartException(string message) : base(message) { }
    }
}
