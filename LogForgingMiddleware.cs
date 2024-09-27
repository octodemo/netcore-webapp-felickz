using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class LogForgingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogForgingMiddleware> _logger;

    public enum RbacRole
    {
        Admin,
        User,
        Guest
    }


    public LogForgingMiddleware(RequestDelegate next, ILogger<LogForgingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }



    public async Task InvokeAsync(HttpContext context)
    {
        string role = context.Request.Query["role"];

        // BAD: User input logged as-is
        _logger.LogWarning(role + " log in requested.");

        // GOOD: User input logged with new-lines removed
        _logger.LogWarning(role?.Replace(Environment.NewLine, "") + " log in requested");

        // GOOD: cast to enum
        _logger.LogWarning((RbacRole)Enum.Parse(typeof(RbacRole), role) + " log in requested");

        // GOOD: cast to enum / tryparse
        if (Enum.TryParse(typeof(RbacRole), role, out var parsedRole))
        {
            _logger.LogWarning("{Role} log in requested.", (RbacRole)parsedRole);
        }
        else
        {
            // GOOD: User input logged with new-lines removed
            _logger.LogWarning(role?.Replace(Environment.NewLine, "") + " log in requested");
        }

        await _next(context);
    }
}
