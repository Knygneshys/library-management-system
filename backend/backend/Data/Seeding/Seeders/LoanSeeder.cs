using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class LoanSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Loans.AnyAsync()) return;

        var reservation = await context.Reservations.FirstOrDefaultAsync();

        if (reservation is null) return;

        var copy = await context.Copies
            .Where(c => c.BookId == reservation.BookId && !c.IsTaken)
            .FirstOrDefaultAsync();

        if (copy is null)
        {
            copy = await context.Copies.Where(c => c.BookId == reservation.BookId).FirstOrDefaultAsync();
        }

        if (copy is null) return;

        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            CopyId = copy.Id,
            ReservationId = reservation.Id,
            LoanDate = DateTime.UtcNow,
            ReturnDate = null//DateTime.UtcNow.AddDays(14)
        };

        copy.IsTaken = true;

        context.Loans.Add(loan);
        context.Copies.Update(copy);

        await context.SaveChangesAsync();
    }
}