using Features.Auth.Types;

namespace Features.Transactions.Types;

public record Sale(
    Guid Id,
    User CreatedBy,
    decimal Total,
    DateTime CreatedAt,
    DateTime UpdatedAt);