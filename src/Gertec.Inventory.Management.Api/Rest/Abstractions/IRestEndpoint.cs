namespace Gertec.Inventory.Management.Api.Abstractions;

public interface IRestEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}