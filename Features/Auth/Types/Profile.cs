namespace Features.Auth.Types;

public record Profile(
    string FirstName,
    string LastName,
    DateTime BirthDate
);