namespace backend.Exceptions.AlreadyExists;

public class EntityByNameAlreadyExistsException : Exception
{
    public EntityByNameAlreadyExistsException ()
    {
    }

    public EntityByNameAlreadyExistsException (string name)
        : base($"\"{name}\" already exists!")
    {
    }

    public EntityByNameAlreadyExistsException (string message, Exception inner)
        : base(message, inner)
    {
    }
}