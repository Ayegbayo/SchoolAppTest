using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolApp.API.Middlewares
{
    public class ExceptionMiddleware
    {
        public static string LocalizationKey => "LocalizationKey";

        private readonly RequestDelegate _next;
        private  readonly ILogger<ExceptionMiddleware> Logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, $"error occured", context.Request.Path.Value);
                var response = context.Response;                
                response.ContentType = "application/json";
                var responseContent = new APIResponse { StatusCode = 500, IsError = true, Message = "An unknown error occured while processing your request. Please try again later"};
                response.StatusCode = (int)responseContent.StatusCode;
                await response.WriteAsync(JsonSerializer.Serialize(responseContent));
            }
        }
    }
}
