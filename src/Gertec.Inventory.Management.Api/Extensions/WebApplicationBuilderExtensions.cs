using System.Reflection;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common.Helpers;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Database;
using Gertec.Inventory.Management.Infrastructure.Database.Mappers;
using Gertec.Inventory.Management.Api.Resources;

namespace Gertec.Inventory.Management.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string DbConnectionstringConfigName = "DEFAULT_CONNECTION";
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
       
        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
        });
    }
    
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
       
        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
        });
    }
    
    private static void AddDatabaseAndRepositories(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(DbConnectionstringConfigName);

        if (connectionString is null)
            throw new ArgumentException(ConfigurationMessages.MissingConfiguration, DbConnectionstringConfigName);
        
        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
            config.AddMap(new TransactionMap());
            config.AddMap(new DailyInventoryMap());
            config.ForDommel();
        });
        
        builder.Services.AddScoped<IDbContext, DbContext>(provider => new DbContext(connectionString));
        builder.Services.AddScoped<IItemRepository, IItemRepository>();
        builder.Services.AddScoped<IItemRepository, IItemRepository>();
    }
}