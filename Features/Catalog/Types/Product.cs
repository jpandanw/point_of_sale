namespace Features.Catalog.Types;

public record Product(
    Guid Id,
    string Name,
    string Description,
    Category Category,
    Variant[] Variants,
    DateTime CreatedAt,
    DateTime UpdatedAt
);