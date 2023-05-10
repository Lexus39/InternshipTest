using System.Runtime.CompilerServices;

namespace InternshipTest.API.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
