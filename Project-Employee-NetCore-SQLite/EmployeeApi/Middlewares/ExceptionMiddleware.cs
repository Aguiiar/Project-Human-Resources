using System.Net;
using System.Text.Json;
using EmployeeApi.Exceptions;

namespace EmployeeApi.Middlewares
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
            catch (AppException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var response = new
                {
                    status = ex.StatusCode,
                    message = ex.Message
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            catch (Exception)
            {
                context.Response.ContentType= "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    status = 500,
                    message = "Interna Server error"
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }

        }
    }
}
