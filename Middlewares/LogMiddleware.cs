using System.Text;
using Microsoft.AspNetCore.Http;

namespace TodoApi.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="https://medium.com/@xiuqige/net-core-%E5%88%A9%E7%94%A8middleware%E5%AE%8C%E6%88%90request-response-exception-log-6df55f059dfe"/>
    /// <seealso cref="https://sulhome.com/blog/10/log-asp-net-core-request-and-response-using-middleware"/>
    /// <seealso cref="https://www.c-sharpcorner.com/article/global-error-handling-in-asp-net-core-web-api-using-nlog/"/>
    public class LogMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }
    }
}