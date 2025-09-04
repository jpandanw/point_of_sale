using Features.Auth.Types;

namespace Features.Catalog.Types;

public record Inventory(
    Guid Id,
    string Name,
    string Description,
    int    InventoryChange,
    InventoryChangeReason InventoryChangeReason,
    User CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt );