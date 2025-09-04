using Features.Shared;

namespace Features.Catalog.Types;


public record PriceModifier (
    Guid Id,
    string Name,
    string Description,
    decimal ModifierValue,
    ModifierType ModifierType,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    
}