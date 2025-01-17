﻿using Cart.Entities.ErrorModels;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Cart.Api.Middlewares;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeatures != null)
                {
                    context.Response.StatusCode = contextFeatures.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        AuthFaildException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    var response = new ErrorDetails(
                        context.Response.StatusCode,
                        contextFeatures.Error.Message
                    );

                    await context.Response.WriteAsync(response.ToString());
                }
            });
        });
    }
}
