using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCustomer.Middleware
{
    public static class LoggerHandlerExtensions
    {
        /// <summary>
        /// logger middleware 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebAPILogger(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentException(nameof(app));
            }
            return app.UseMiddleware<LoggerMiddleware>();
        }
    }
}
