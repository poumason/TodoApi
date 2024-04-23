using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApi.Middlewares
{
    public class HeaderCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 檢查請求中是否存在 "MyHeader" 標頭
            if (!context.Request.Headers.ContainsKey("MyHeader"))
            {
                // 如果標頭不存在，返回 400 BAD REQUEST
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Missing required header: MyHeader");
            }
            else
            {
                // 如果標頭存在，繼續執行下一個 middleware
                await _next(context);
            }
        }
    }

}