using System.Reflection;
using Gertec.Inventory.Management.Api.Rest.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gertec.Inventory.Management.Api.Extensions;

public static class RestEndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IRestEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IRestEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IRestEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IRestEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IRestEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}