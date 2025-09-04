namespace Features.Catalog.Variant.Create;

public record VariantCreateRequest(
    string Name,
    Guid    ProductId,
    string? Description,
    decimal BasePrice,
    int?    InitialStock,
    string? Sku,
    string? BarCode
);