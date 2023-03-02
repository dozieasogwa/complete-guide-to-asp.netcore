using My_books.Data.ViewModels;
using System.Net;

namespace My_books.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpcontext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpcontext.Response.ContentType = "application/json";

            var response = new ErrorVM()
            {
                StatusCode = httpcontext.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware",
                Path = "path-goes-here"
            };

            return httpcontext.Response.WriteAsync(response.ToString());
        }
    }
}
