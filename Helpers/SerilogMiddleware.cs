using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinMejoraWebApi.Helpers
{
    public class SerilogAddUserInfo
    {
        private readonly RequestDelegate _next;

        public SerilogAddUserInfo(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("Address", context.Connection.RemoteIpAddress)) ;
            using (LogContext.PushProperty("Session", context.Connection.RemoteIpAddress)) ;
            //using (LogContext.PushProperty("Session", context.Session.GetString("SessionGUID") ?? "Unknown"))
            //{
            //    await _next.Invoke(context);
            //}
        }
    }
}
