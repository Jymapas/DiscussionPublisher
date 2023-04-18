using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DiscussionPublisherAPI
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");

                if (httpContext.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error page middleware will not be executed.");
                    throw;
                }

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Internal Server Error"
                };

                switch (ex)
                {
                    case ArgumentException argumentException:
                        _logger.LogWarning($"Invalid argument: {argumentException.Message}");
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = argumentException.Message;
                        break;
                    case NotImplementedException notImplementedException:
                        _logger.LogWarning($"Not implemented: {notImplementedException.Message}");
                        errorResponse.StatusCode = (int)HttpStatusCode.NotImplemented;
                        errorResponse.Message = notImplementedException.Message;
                        break;
                    case TimeoutException timeoutException:
                        _logger.LogWarning($"Timeout occurred: {timeoutException.Message}");
                        errorResponse.StatusCode = (int)HttpStatusCode.RequestTimeout;
                        errorResponse.Message = timeoutException.Message;
                        break;
                    case ValidationException validationException:
                        _logger.LogWarning($"Validation failed: {validationException.Message}");
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = validationException.Message;
                        break;
                    case UnauthorizedAccessException:
                        _logger.LogWarning("Unauthorized access");
                        errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.Message = "Unauthorized Access";
                        break;
                    case InvalidOperationException:
                        _logger.LogWarning($"Invalid operation: {ex.Message}");
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = ex.Message;
                        break;
                    case not null:
                        _logger.LogError($"Unexpected error occurred: {ex}");
                        errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = "An unexpected error occurred";
                        break;
                }

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
