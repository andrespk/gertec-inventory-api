using Gertec.Inventory.Management.Api;
using Gertec.Inventory.Management.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();