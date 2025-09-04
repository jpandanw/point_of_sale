namespace Features.Auth.Types;

public record User(
    Guid Id,
    string Username,
    Profile Profile,
    Role[] Roles,
    DateTime CreatedAt,
    DateTime UpdatedAt );
    
    