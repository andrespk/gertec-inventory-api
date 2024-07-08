using System.Reflection;
using Asp.Versioning;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Gertec.Inventory.Management.Api.Resources;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Gertec.Inventory.Management.Infrastructure.Data.Helpers;
using Gertec.Inventory.Management.Infrastructure.Data.Mappers;
using Gertec.Inventory.Management.Infrastructure.Data.Repositories;
using Gertec.Inventory.Management.Infrastructure.Logging;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;
using Serilog;

namespace Gertec.Inventory.Management.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder, ApiVersion apiVersion)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddApiVersioningAndEndpoints(apiVersion, builder.Configuration);
        builder.Services.AddLogging(builder.Configuration);
        builder.Services.AddEndpoints(typeof(Program).Assembly);
        builder.Services.AddDbCapabilitiesAndRepositories();
    }

    private static void AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>(DataHelper.DefaultConnectionConfigName);
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.MySQL(connectionString, DataHelper.LogTableName, storeTimestampInUtc: true)
            .CreateLogger();
    }

    private static void AddApiVersioningAndEndpoints(this IServiceCollection services, ApiVersion apiVersion, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = apiVersion;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat =
                configuration.GetValue<string>(ApiConstants.VersioningGroupNameFormatConfigName) ?? "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
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