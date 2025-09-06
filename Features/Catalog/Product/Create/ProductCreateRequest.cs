namespace Features.Catalog.Product.Create;

public record ProductCreateRequest(
    Guid? Id ,
    string Name,
    string? Description,
    Guid? CategoryId
);