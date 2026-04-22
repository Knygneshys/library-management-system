namespace backend.Exceptions;

public class PrintingHouseHasBookException : Exception
{
    public PrintingHouseHasBookException()
    {
    }

    public PrintingHouseHasBookException(string name)
        : base($"{name} has books and therefore cannot be deleted!")
    {
    }

    public PrintingHouseHasBookException(string message, Exception innerException) : base(message, innerException)
    {
    }
}