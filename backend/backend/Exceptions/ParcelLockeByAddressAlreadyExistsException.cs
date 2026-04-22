namespace backend.Exceptions;

public class ParcelLockerByAddressAlreadyExistsException : Exception
{
    public ParcelLockerByAddressAlreadyExistsException()
    {
    }

    public ParcelLockerByAddressAlreadyExistsException(string address)
        : base($"Parcel locker with address \"{address}\" already exists!")
    {
    }

    public ParcelLockerByAddressAlreadyExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}