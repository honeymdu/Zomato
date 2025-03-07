using System.Net;

namespace Zomato.Advices
{
    public class ApiError
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }

        public ApiError(int httpStatus, string message)
        {
            HttpStatus = httpStatus;
            Message = message;
        }
    }
}
