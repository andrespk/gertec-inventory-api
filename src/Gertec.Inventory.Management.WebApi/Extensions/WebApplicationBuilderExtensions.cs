using System.Reflection;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Gertec.Inventory.Management.Infrastructure.Database.Mappers;

namespace Gertec.Inventory.Management.WebApi.Extensions;

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
        
        FluentMapper.Initialize(config =>
        {
            config.AddMap(new ItemMap());
            config.AddMap(new TransactionMap());
            config.AddMap(new DailyInventoryMap());
            config.ForDommel();
        });
    }
}