using System.Data.Common;
using Infrastructure.Db;
using SqlKata.Execution;

namespace Features.Catalog.Product.Create;

public class ProductCreateHandler(AppDbContext ctx)
{
    private readonly AppDbContext _ctx = ctx;

    public async Task<ProductCreateResponse> HandleAsync(ProductCreateRequest request)
    {

        await _ctx.Catalog.Products.InsertAsync(new
        {
            
        });
        return new ProductCreateResponse();
    }
}