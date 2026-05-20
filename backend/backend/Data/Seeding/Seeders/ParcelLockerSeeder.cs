using backend.Data;
using backend.Models;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class ParcelLockerSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.ParcelLockers.AnyAsync()) return;

        var parcelLocker = new ParcelLocker
        {
            Id = Guid.NewGuid(),
            Address = "Studentų g. 50, Kaunas",
            LockerState = ParcelLockerState.Active
        };
        context.ParcelLockers.Add(parcelLocker);

        var lockers = new List<Locker>
        {
            new Locker {
                Id = Guid.NewGuid(),
                ParcelLockerId = parcelLocker.Id,
                LocationCode = "A1",
                Height = 20.0, 
                Width = 30.0,
                Length = 50.0,
                LockerState = LockerState.Empty,
                IsDoorClosed = true
            },  
            new Locker {
                Id = Guid.NewGuid(),
                ParcelLockerId = parcelLocker.Id,
                LocationCode = "A2",
                Height = 20.0, 
                Width = 30.0,
                Length = 50.0,
                LockerState = LockerState.Empty,
                IsDoorClosed = true
            }  
        };
        context.Lockers.AddRange(lockers);

        await context.SaveChangesAsync();
    }
}