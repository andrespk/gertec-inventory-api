namespace Gertec.Inventory.Management.WebApi.Abstractions;

public interface IRestEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}