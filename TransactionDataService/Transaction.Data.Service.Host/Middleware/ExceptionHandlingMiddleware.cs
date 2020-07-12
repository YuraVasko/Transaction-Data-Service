using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Service.BLL.Exceptions;

namespace Transaction.Data.Service.Host.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(TransactionBaseException ex)
            {
                await ProccessException(ex.Message, HttpStatusCode.BadRequest, context);
            }
            catch (Exception ex)
            {
                const string PublicErrorMessage = "Server error";
                await ProccessException(ex.Message, PublicErrorMessage,HttpStatusCode.BadRequest, context);
            }
        }
        private Task ProccessException(string message, HttpStatusCode statusCode, HttpContext context)
        {
            return ProccessException(message, message, statusCode, context);
        }

        private async Task ProccessException(string logMessage, string responseMessage, HttpStatusCode statusCode, HttpContext context)
        {
            _logger.LogInformation(logMessage);
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(responseMessage, Encoding.UTF8);
        }
    }
}
