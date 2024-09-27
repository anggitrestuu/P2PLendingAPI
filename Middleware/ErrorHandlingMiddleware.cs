using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace P2PLendingAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == 401)
                {
                    await HandleUnauthorizedAsync(context);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleUnauthorizedAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = 401,
                message = "Unauthorized. Please login to access this resource."
            }));
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception has occurred.");

            var code = HttpStatusCode.InternalServerError;
            var message = "An error occurred while processing your request.";

            switch (exception)
            {
                case KeyNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;
                case UnauthorizedAccessException _:
                    code = HttpStatusCode.Forbidden;
                    // check if the exception message is not empty
                    message = string.IsNullOrWhiteSpace(exception.Message) ? "Unauthorized." : exception.Message;
                    break;
                case InvalidOperationException _:
                    code = HttpStatusCode.BadRequest;
                    message = "Invalid operation.";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = (int)code,
                message = message
            }));
        }
    }
}