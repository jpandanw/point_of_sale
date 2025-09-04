namespace Features.Catalog.Product.Create;

public record ProductCreateRequest(
    string Name,
    string? Description,
    Guid? CategoryId
);