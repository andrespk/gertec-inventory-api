namespace Gertec.Inventory.Management.Api.Rest.Abstractions;

public interface IRestEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}