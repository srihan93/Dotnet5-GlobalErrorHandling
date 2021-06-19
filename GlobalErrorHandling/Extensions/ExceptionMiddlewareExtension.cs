using GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is System.ArgumentNullException)
                        {
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Message = "the inputs is null"
                            }.ToString());
                        }
                        else if (contextFeature.Error is CustomBadRequest)
                        {
                            const int badRequest = (int)HttpStatusCode.BadRequest;
                            context.Response.StatusCode = badRequest;
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = badRequest,
                                Message = contextFeature.Error.Message
                            }.ToString());
                        }
                        else if (contextFeature.Error is ItemNotFound)
                        {
                            const int notFound = (int)HttpStatusCode.NotFound;
                            context.Response.StatusCode = notFound;
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Message = contextFeature.Error.Message
                            }.ToString());
                        }
                        else
                        {
                            const int serverError = (int)HttpStatusCode.InternalServerError;
                            context.Response.StatusCode = serverError;
                            await context.Response.WriteAsync(new GlobalErrorHandling.Models.ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                Message = "technical error occured,"
                            }.ToString());
                        }
                    }
                }
                    );
            });
        }
    }
}