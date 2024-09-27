using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class LogForgingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogForgingMiddleware> _logger;

    public LogForgingMiddleware(RequestDelegate next, ILogger<LogForgingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string username = context.Request.Query["username"];
        // BAD: User input logged as-is
        _logger.LogWarning(username + " log in requested.");
        // GOOD: User input logged with new-lines removed
        _logger.LogWarning(username?.Replace(Environment.NewLine, "") + " log in requested");

        await _next(context);
    }
}
