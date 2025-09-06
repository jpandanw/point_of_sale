namespace Features.Catalog.Product.Create;

public record ProductCreateResponse(
    Guid Id,
    string Name,
    string Description,
    Guid? CategoryId,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt );