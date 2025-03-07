namespace Zomato.Advices
{
    public class ApiResponse<T>
    {
        public DateTime Timestamp { get; set; }
        public T Data { get; set; }
        public T Error { get; set; }
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
        public ApiResponse(T error, string message)
        {
            Timestamp = DateTime.UtcNow;
            this.Error = error;
            Data = default;
        }

        // Default constructor
        public ApiResponse()
        {
            Timestamp = DateTime.Now;
        }
    }

}
