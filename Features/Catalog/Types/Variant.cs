namespace Features.Catalog.Types;



public record Variant(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string? Sku,
    string? BarCode,
    string? CurrentStocks,
    DateTime CreatedAt,
    DateTime UpdatedAt
);


