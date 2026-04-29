namespace backend.Exceptions;

public class ParcelLockerByIdDoesNotExists : Exception
{
    public ParcelLockerByIdDoesNotExists()
    {
    }

    public ParcelLockerByIdDoesNotExists(Guid id)
        : base($"Parcel locker with id \"{id}\" does not exists!")
    {
    }

    public ParcelLockerByIdDoesNotExists(string message, Exception inner)
        : base(message, inner)
    {
    }
}