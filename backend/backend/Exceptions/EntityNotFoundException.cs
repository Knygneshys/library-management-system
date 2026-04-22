namespace backend.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException ()
    {
    }

    public EntityNotFoundException (string name)
        : base($"\"{name}\" not found!")
    {
    }

    public EntityNotFoundException (string message, Exception inner)
        : base(message, inner)
    {
    }
}