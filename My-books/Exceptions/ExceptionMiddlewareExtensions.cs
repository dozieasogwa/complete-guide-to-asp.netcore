using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using My_books.Data.ViewModels;
using System.Net;

namespace My_books.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureBuildInExceotionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async Context =>
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Context.Response.ContentType = "application/json";

                    var contextFeature = Context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = Context.Features.Get<IHttpRequestFeature>();

                    if (Context.Features != null)
                    {
                        await Context.Response.WriteAsync(new ErrorVM()
                        {
                            StatusCode = Context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path
                        }.ToString());
                    }
                });
            });
        }
      
        public static void configureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
