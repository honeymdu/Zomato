namespace Zomato.Exceptions.CustomExceptionHandler
{
    public class IllegalArgumentException:Exception
    {
        public IllegalArgumentException(string message) : base(message) { }
    }
}
