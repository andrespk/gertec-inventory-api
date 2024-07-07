using System.Reflection;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Gertec.Inventory.Management.Api.Resources;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data;
using Gertec.Inventory.Management.Infrastructure.Data.Mappers;
using Gertec.Inventory.Management.Infrastructure.Data.Repositories;
using Gertec.Inventory.Management.Infrastructure.Logging;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;

namespace Gertec.Inventory.Management.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string DbConnectionstringConfigName = "DEFAULT_CONNECTION";

    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddServices();
    }

    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.RegisterDatabaseAndRepositoriesServices();
    }

    private static void RegisterDatabaseAndRepositoriesServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(DbConnectionstringConfigName);

        if (connectionString is null)
            throw new ArgumentException(ConfigurationMessages.MissingConfiguration, DbConnectionstringConfigName);

        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
            config.AddMap(new TransactionMap());
            config.AddMap(new DailyInventoryMap());
            config.AddMap(new ApplicationLogMap());
            config.ForDommel();
        });

        builder.Services.AddScoped<IDbContext, DbContext>(provider => new DbContext(connectionString));
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<IDailyInventoryRepository, DailyInventoryRepository>();
        builder.Services.AddScoped<IApplicationLogRepository, ApplicationLogRepository>();
    }
}