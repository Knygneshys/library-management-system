using backend.Data;
using backend.Dtos.Book;
using backend.Services.Interfaces;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class BookServices(LibraryDbContext dbContext) : IBookServices
{
    public async Task<List<BookDto>> GetBooksAsync()
    {
         return await dbContext.Books
            .Select(b => new BookDto()
            {
                Id = b.Id,
                Title = b.Title,
                Summary = b.Summary,
                Isbn = b.Isbn,
                Language = b.Language,
                PublishedAt = b.PublishedAt,
                Author = b.Author.FullName,
                PrintingHouse = b.PrintingHouse.Name,
                Genres = b.Genres.Select(g => g.Title).ToList(),
                Publisher = b.Publisher.Name,
                ActiveReservation = b.Reservations
                    .Where(r => r.State == ReservationState.InQueue || r.State == ReservationState.InProgress || r.State == ReservationState.NotLate || r.State == ReservationState.Late)
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => new ReservationDto { Id = r.Id, State = r.State })
                    .FirstOrDefault()
            })
            .ToListAsync();
    }
}