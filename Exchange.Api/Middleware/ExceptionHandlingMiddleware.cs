using Exchange.Application.Responses;

namespace Exchange.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business exception occurred");
                await HandleExceptionAsync(context, ex.Message, 400, "Business error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                await HandleExceptionAsync(context, "Internal server error", 500, ex.Message);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, string error, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = ApiResponse<string>.FailResponse(error, message);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
