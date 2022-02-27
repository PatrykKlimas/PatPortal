using PatPortal.Domain.Exceptions;
using System.Net;

namespace PatPortal.API.CustomMiddlewere
{
    public class ApiErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _enviroment;

        public ApiErrorHandler(RequestDelegate next, IWebHostEnvironment enviroment)
        {
            _next = next;
            _enviroment = enviroment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception exception)
        {
            var response = MapCustomExceptions(exception);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.StatusCode;

            return httpContext.Response.WriteAsync(response.ErrorMessage);
        }

        private (int StatusCode, string ErrorMessage) MapCustomExceptions(Exception exception)
        {
            if (exception.GetType() == typeof(DomainValidationException) ||
                exception.GetType() == typeof(InitValidationException) ||
                exception.GetType() == typeof(CustomValidationnException))
            {
                return ((int)HttpStatusCode.BadRequest, exception.Message);
            }

            if (exception.GetType() == typeof(EntityNotFoundException))
                return ((int)HttpStatusCode.NotFound, exception.Message);

            return ((int)HttpStatusCode.InternalServerError,
                  _enviroment.IsProduction() ? "Internal server error" : "Error: " + exception.Message);

        }
    }
}
