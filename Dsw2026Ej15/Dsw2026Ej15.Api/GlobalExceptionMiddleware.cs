using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400; 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Problem" });
            }
        }
    }
}
