namespace Zomato.Advices
{
    public class ApiResponse<T>
    {
        public DateTime Timestamp { get; set; }
        public T Data { get; set; }
        public ApiError Error { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        // Constructor for success response
        public ApiResponse(T data, string message = "Success", bool success = true)
        {
            Timestamp = DateTime.Now;
            Data = data;
            Message = message;
            Success = success;
        }

        // Constructor for error response
        public ApiResponse(ApiError error)
        {
            Timestamp = DateTime.Now;
            Error = error;
            Success = false;
        }

        // Default constructor
        public ApiResponse()
        {
            Timestamp = DateTime.Now;
        }
    }

}
