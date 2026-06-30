using System.Diagnostics;

namespace OrayPortfolio.Web.Middlewares;

public class VisitorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<VisitorLoggingMiddleware> _logger;

    public VisitorLoggingMiddleware(RequestDelegate next, ILogger<VisitorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        await _next(context);

        sw.Stop();

        var path = context.Request.Path.Value;

        // .css, .js, .png gibi statik dosya isteklerini loglamayıp sadece sayfa ziyaretlerini alıyoruz
        if (!string.IsNullOrEmpty(path) && !path.Contains('.'))
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var method = context.Request.Method;
            var statusCode = context.Response.StatusCode;
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            _logger.LogInformation(
                "Ziyaretçi: {IpAddress} | {Method} {Path} | Durum: {StatusCode} | Süre: {ElapsedMilliseconds}ms | Tarayıcı: {UserAgent}",
                ipAddress, method, path, statusCode, sw.ElapsedMilliseconds, userAgent);
        }
    }
}