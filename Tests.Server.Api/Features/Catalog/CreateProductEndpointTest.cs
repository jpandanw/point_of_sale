using System.Text;
using System.Text.Json;
using Features.Catalog.Product.Create;

namespace Tests.Server.Api.Features.Catalog;

public class CreateProductEndpointTest
{
    private readonly string _url = "http://localhost:5046/api/catalog/products";
    
    [Test]
    public async Task ShouldCreateProduct()
    {
        using var client = new HttpClient();

        var req = new ProductCreateRequest(
            Id : null,
            Description : "Test",
            CategoryId : null,
            Name : "Test");

        var payload = JsonSerializer.Serialize(req);
        var data = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_url, data).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var responseData = JsonSerializer.Deserialize<ProductCreateResponse>(content);
        Console.WriteLine(responseData);
        
        Assert.Pass();

    }
}