using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace InternshipTest.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception) 
            {
                await HandleException(context, exception);
            }
        }

        public Task HandleException(HttpContext context, Exception exception)
        {
            var status = HttpStatusCode.InternalServerError;
            var message = "Internal error";
            if (exception.GetType() == typeof(ArgumentException))
            {
                message = exception.Message;
                var notFoundMessagePattern = "not found";
                if (Regex.IsMatch(message, notFoundMessagePattern))
                    status = HttpStatusCode.NotFound;
                else
                    status = HttpStatusCode.BadRequest;
            }
            if (exception.GetType() == typeof(DbUpdateException))
            {
                message = "DbUpdateException";
                status = HttpStatusCode.BadRequest;
            }
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(message);
        }
    }
}
