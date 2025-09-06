namespace Features.Shared.Exceptions;

public class ForeignKeyNotExists : Exception
{
    public  ForeignKeyNotExists() {}
    
    public  ForeignKeyNotExists(string message) : base(message) {}
    
    public  ForeignKeyNotExists(string message, Exception inner) : base(message, inner) {}
    
}