using Asp.Versioning;
using Asp.Versioning.Builder;
using Gertec.Inventory.Management.Api;
using Gertec.Inventory.Management.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var defaultVersion = new ApiVersion(
    builder.Configuration.GetValue<int?>(ApiConstants.DefaultEndpointVersionConfigName) ??
    1);

builder.AddInfrastructure(defaultVersion);

var app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(defaultVersion)
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();