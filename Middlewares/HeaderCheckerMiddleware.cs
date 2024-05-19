using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace TodoApi.Middlewares
{
    public class HeaderCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;

        public HeaderCheckerMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _cache.SetAsync("header", Encoding.UTF8.GetBytes("hello"));
            // 檢查請求中是否存在 "MyHeader" 標頭
            // if (!context.Request.Headers.ContainsKey("MyHeader"))
            // {
            //     // 如果標頭不存在，返回 400 BAD REQUEST
            //     context.Response.StatusCode = 400;
            //     await context.Response.WriteAsync("Missing required header: MyHeader");
            // }
            // else
            // {
                // 如果標頭存在，繼續執行下一個 middleware
                await _next(context);
            // }
        }
    }

}