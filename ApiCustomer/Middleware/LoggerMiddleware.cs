using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCustomer.Middleware
{
    public class LoggerMiddleware
    {
        #region field
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;
        #endregion
        #region cotr
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggerMiddleware>();
        }
        #endregion
        #region method
        /// <summary>
        /// invoke method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ////for the exception logging
               _logger.LogInformation(
                         $"Request {context.Request.Method} {context.Request.Body} => {context.Response.Body}",
                         ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes("Hata oluştu."));
            }
            finally
            {
                ////for the logging response
                //_logger.LogInformation(
                //          "Request {method} {url} => {statusCode}",
                //          context.Request?.Method,
                //          context.Request?.Path.Value,
                //          context.Response?.StatusCode);
            }
        }
        #endregion
    }
}
