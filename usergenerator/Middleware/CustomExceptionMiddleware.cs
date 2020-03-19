using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace usergenerator.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILoggerManager _logger;

        //constructor and service injection
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(
                    new
                    {
                        StatusCodes = httpContext.Response.StatusCode,
                        Message = "Internal server Error from the custom middleware."
                    }.ToString()
                );
        }

        //private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        //{
        //    var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        //    //if (ex is NotFoundException) code = HttpStatusCode.NotFound;
        //    //else if (ex is UnauthorizedException) code = HttpStatusCode.Unauthorized;
        //    //else if (ex is Exception) code = HttpStatusCode.BadRequest;

        //    var result = JsonConvert.SerializeObject(new { error = ex.Message });
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)code;
        //    return context.Response.WriteAsync(result);
        //}
    }
}
