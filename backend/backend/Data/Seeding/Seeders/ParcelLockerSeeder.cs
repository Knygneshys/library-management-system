using backend.Data;
using backend.Enums;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class ParcelLockerSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.ParcelLockers.AnyAsync())
            return;

        var parcelLocker = new ParcelLocker
        {
            Id = Guid.NewGuid(),
            Address = "Studentų g. 50, Kaunas",
            LockerState = ParcelLockerState.Active,
            Lockers = new List<Locker>
            {
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "A1",
                    Height = 30,
                    Width = 40,
                    Length = 50,
                    LockerState = LockerState.Empty
                },
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "A2",
                    Height = 30,
                    Width = 40,
                    Length = 50,
                    LockerState = LockerState.Empty
                },
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "A3",
                    Height = 30,
                    Width = 40,
                    Length = 50,
                    LockerState = LockerState.Empty
                },
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "B1",
                    Height = 50,
                    Width = 50,
                    Length = 60,
                    LockerState = LockerState.Empty
                },
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "B2",
                    Height = 50,
                    Width = 50,
                    Length = 60,
                    LockerState = LockerState.Empty
                },
                new Locker
                {
                    Id = Guid.NewGuid(),
                    LocationCode = "C1",
                    Height = 70,
                    Width = 60,
                    Length = 80,
                    LockerState = LockerState.Empty
                }
            }
        };

        context.ParcelLockers.Add(parcelLocker);
        await context.SaveChangesAsync();
    }
}