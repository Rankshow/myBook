﻿using BookCollect.Services.ViewModels;
using System.Net;

namespace BookCollect.Exceptions
{
    public class CustomeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomeExceptionMiddleware(RequestDelegate next) 
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

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware",
                Path = "The path goes here"
            };

            await httpContext.Response.WriteAsync(response.ToString());    
        }
    }
}
