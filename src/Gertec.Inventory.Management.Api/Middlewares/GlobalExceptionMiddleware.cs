using Gertec.Inventory.Management.Domain.Common.Primitives;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using Result = FastEndpoints.MediatR.Models.Result;

namespace Gertec.Inventory.Management.Api.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogService _logService;

    public GlobalExceptionMiddleware(ILogService logService) => _logService = logService;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (exception.GetType() == typeof(ValidationException))
            {
                _logService.LogWarning(nameof(ValidationException), exception.Message, GetRequestDetails(context));
                var failures = (exception as ValidationException)?.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(k => ResolveDictionaryKey(k.Key), v => v.First().ErrorMessage);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(
                    Result.Failed("Validation error(s) found.", failures,
                        context.Response.StatusCode), serializerOptions);
            }
            else
            {
                _logService.LogError(exception.GetType().Name, exception, GetRequestDetails(context));

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    Result.Failed(exception.Message, null, context.Response.StatusCode), serializerOptions);
            }
        }
    }

    private static string ResolveDictionaryKey(string inputPath)
    {
        var regex = new Regex(@"\[(\d+)\]");
        var convertedPath = regex.Replace(inputPath, ".$1");

        var parts = convertedPath.Split('.');
        for (var i = 1; i < parts.Length; i++)
        {
            parts[i] = char.ToLower(parts[i][0]) + parts[i][1..];
        }

        return string.Join(".", parts);
    }

    private RequestDetails GetRequestDetails(HttpContext httpContext)
    {
        var request = JsonSerializer.Serialize(httpContext.Request);
        var responseStatus = httpContext.Response.StatusCode.ToString();
        return new RequestDetails(request, responseStatus);
    }
}