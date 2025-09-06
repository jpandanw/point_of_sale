using System.Data.Common;
using Infrastructure.Db;
using SqlKata.Execution;

namespace Features.Catalog.Product.Create;

public class ProductCreateHandler(AppDbContext ctx)
{
    public async Task<ProductCreateResponse> HandleAsync(ProductCreateRequest request)
    {

        var productId = request.Id ?? Ulid.NewUlid().ToGuid();
        var createdAt = DateTime.Now;
        var result = await ctx.Catalog.Products.InsertAsync(new
        {
            id = productId,
            name = request.Name,
            description = request.Description ?? "",
            category_id = request.CategoryId,
            status      ="active", // TODO: add enum for status
            created_at  = createdAt,
            updated_at  = createdAt,
            
            
        });
        return new ProductCreateResponse(
            Id: productId,
            Name: request.Name,
            Description: request.Description ?? "",
            CategoryId:   request.CategoryId,
            Status: "active",
            CreatedAt: createdAt,
            UpdatedAt: createdAt
            );
        
    }
}