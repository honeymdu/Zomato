namespace Zomato.Advices
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T data, string message = "Success", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T>(data, message, true);
        }

        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>(default, message, false);
        }
    }

}
