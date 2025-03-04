using System.Net;

namespace Zomato.Advices
{
    public class ApiError
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string Message { get; set; }

        public ApiError(HttpStatusCode httpStatus, string message)
        {
            HttpStatus = httpStatus;
            Message = message;
        }
    }
}
