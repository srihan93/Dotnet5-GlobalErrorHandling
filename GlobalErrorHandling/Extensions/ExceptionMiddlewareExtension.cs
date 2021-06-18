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
                        if(contextFeature.Error is System.ArgumentNullException)
                        {
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Message = "the inputs is null"

                            }.ToString());
                        }
                        else
                        {
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                Message = "technical error occured,"

                            }.ToString()) ;
                        }


                        
                    }
                }
                    );
            });
        }
    }
}
