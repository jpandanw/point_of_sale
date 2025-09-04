using Features.Catalog.Types;

namespace Features.Transactions.Types;

public record SaleItem(
    Guid Id,
    Variant  Product,
    decimal SalePrice,
    decimal SaleModifier
);
