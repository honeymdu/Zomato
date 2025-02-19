using System.Text.Json;

namespace Zomato.Advices
{
    public class GlobalResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var newBodyStream = new MemoryStream();
            context.Response.Body = newBodyStream;

            await _next(context);

            context.Response.Body = originalBodyStream;
            newBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();

            if (!string.IsNullOrEmpty(responseBody))
            {
                var wrappedResponse = new ApiResponse<string>(responseBody, "Request processed successfully", true);
                var jsonResponse = JsonSerializer.Serialize(wrappedResponse);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }

}
