using Features.Catalog.Product.Create;
using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Tests.Features.Features.Catalog;

public class ProductCreateTest
{


    [Test]
    public async Task CreateTest()
    {
        var db = TestAppDbContext.Get();

        var request = new ProductCreateRequest(
            Id: null,
            Name: "Skooma",
            Description: null,
            CategoryId:null
        );

        var product = await new ProductCreateHandler(db).HandleAsync(request);
        
        
        Assert.Pass();
    }
}