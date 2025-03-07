using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Zomato.Exceptions;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Advices
{
    public class GlobalResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalResponseMiddleware> _logger;

        public GlobalResponseMiddleware(RequestDelegate next, ILogger<GlobalResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

      

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var newBodyStream = new MemoryStream();
            context.Response.Body = newBodyStream;

            try
            {
                await _next(context);
                context.Response.Body = originalBodyStream; // Restore original stream before writing response

                newBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();

                if (!string.IsNullOrEmpty(responseBody))
                {
                    object parsedBody;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                context.Response.Body = originalBodyStream; // Ensure original stream is restored in case of error
                await HandleExceptionAsync(context, ex);
            }
        }



        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response has already started, skipping exception handling.");
                return;
            }

            context.Response.Clear(); // Clear the response before writing to avoid stream errors
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                IllegalArgumentException => (int)HttpStatusCode.BadRequest,
                ResourceNotFoundException => (int)HttpStatusCode.NotFound,
                RuntimeConfilictException => (int)HttpStatusCode.Conflict,
                UserNotFoundException => (int)HttpStatusCode.Unauthorized,
                InvalidCartException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var response = new ApiError(statusCode, exception.Message);
            var wrappedresponce = new ApiResponse<ApiError>(response,exception.Message);


            var jsonResponse = JsonSerializer.Serialize(wrappedresponce, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }


    }
}
