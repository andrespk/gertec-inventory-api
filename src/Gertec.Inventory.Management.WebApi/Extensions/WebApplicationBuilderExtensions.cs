using System.Reflection;
using Dapper.FluentMap;
using Gertec.Inventory.Management.Infrastructure.Database.Mappers;

namespace Gertec.Inventory.Management.WebApi.Extensions;

public static class WebApplicationBuilderExtensions
{
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
}