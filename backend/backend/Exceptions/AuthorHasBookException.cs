namespace backend.Exceptions;

public class AuthorHasBookException : Exception
{
    public AuthorHasBookException()
    {
    }

    public AuthorHasBookException(string name) 
        : base($"{name} has books and therefore cannot be deleted!")
    {
    }

    public AuthorHasBookException(string message, Exception innerException) : base(message, innerException)
    {
    }
}