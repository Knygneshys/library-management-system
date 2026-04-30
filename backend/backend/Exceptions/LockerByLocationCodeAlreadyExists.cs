namespace backend.Exceptions;

public class LockerByLocationCodeAlreadyExists : Exception
{
    public LockerByLocationCodeAlreadyExists()
    {
    }

    public LockerByLocationCodeAlreadyExists(string loationCode)
        : base($"Locker with location code \"{loationCode}\" already exists!")
    {
    }

    public LockerByLocationCodeAlreadyExists(string message, Exception inner)
        : base(message, inner)
    {
    }
}