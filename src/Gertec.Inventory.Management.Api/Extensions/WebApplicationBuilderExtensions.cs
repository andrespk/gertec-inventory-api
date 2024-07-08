using System.Reflection;
using Asp.Versioning;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Gertec.Inventory.Management.Api.Resources;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Gertec.Inventory.Management.Infrastructure.Data.Mappers;
using Gertec.Inventory.Management.Infrastructure.Data.Repositories;
using Gertec.Inventory.Management.Infrastructure.Logging;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;

namespace Gertec.Inventory.Management.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddApiVersioningAndEndpoints();
        builder.Services.AddDbCapabilitiesAndRepositories();
        builder.Services.AddLogging();
    }

    private static void AddLogging(this IServiceCollection services)
    {
        services.AddTransient<ILogService, LogService>();
    }

    private static void AddApiVersioningAndEndpoints(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpoints(typeof(Program).Assembly);
    }

    private static void AddDbCapabilitiesAndRepositories(this IServiceCollection services)
    {
        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
            config.AddMap(new TransactionMap());
            config.AddMap(new DailyInventoryMap());
            config.AddMap(new ApplicationLogMap());
            config.ForDommel();
        });

        services.AddScoped<IDbSession, DbSession>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IDailyInventoryRepository, DailyInventoryRepository>();
        services.AddTransient<IApplicationLogRepository, ApplicationLogRepository>();
    }
}