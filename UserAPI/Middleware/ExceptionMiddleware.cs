using System.Net;

namespace UserAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new Error()
                {
                    Message = ex.Message,
                    StatusCode = context.Response.StatusCode.ToString()
                };

                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
