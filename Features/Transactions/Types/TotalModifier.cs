using Features.Shared;

namespace Features.Transactions.Types;

public record TotalModifier(
    Guid Id,
    string Name,
    string Description,
    decimal ModifierValue,
    ModifierType ModifierType);