using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context=>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature!=null)
                    {
                        await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Unexpected Internal Server Error"
                        }.ToString()); 
                    }
                }
                    );
            });
        }
    }
}
