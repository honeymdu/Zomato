using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

            // Reset stream position before reading
            newBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();

            // Restore original body stream
            context.Response.Body = originalBodyStream;

            if (!string.IsNullOrEmpty(responseBody))
            {
                object? parsedBody;
                try
                {
                    parsedBody = JsonSerializer.Deserialize<object>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch
                {
                    parsedBody = responseBody; 
                }

                var wrappedResponse = new ApiResponse<object>(parsedBody, "Request processed successfully", true);
                var jsonResponse = JsonSerializer.Serialize(wrappedResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
