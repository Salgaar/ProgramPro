using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "ApiKey";
    private readonly string ExpectedApiKey = "ProgramPro_#DONT#TOUCH#THIS.IS^^VERY**-12394827523235123.23.423,2134#RESTRICTED_API.Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey))
        {
            if (!context.Request.Path.ToString().ToLower().StartsWith("/_configuration"))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API key is missing.");
                return;
            }
        }

        if (!string.Equals(apiKey, ExpectedApiKey, StringComparison.Ordinal))
        {
            if (!context.Request.Path.ToString().ToLower().StartsWith("/_configuration"))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }
        }

        // The API key is valid; continue to the next middleware or controller action.
        await _next(context);
    }
}

public static class ApiKeyMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}