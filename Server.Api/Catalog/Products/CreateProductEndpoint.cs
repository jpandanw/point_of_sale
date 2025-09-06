using FastEndpoints;
using Features.Catalog.Product.Create;
using Infrastructure.Db;

namespace Api.Catalog.Products;

public class CreateProductEndpoint(AppDbContext dbContext) 
    : Endpoint<ProductCreateRequest, ProductCreateResponse>
{
    public override void Configure()
    {
        Post("/api/catalog/products");
        AllowAnonymous(); // TODO: Remove Anonymous access
    }

    public override async Task HandleAsync(ProductCreateRequest request, CancellationToken ct)
    {
        var handler = new ProductCreateHandler(dbContext);
        var response = await handler.HandleAsync(request);
        await Send.OkAsync( response, ct);
    }
}