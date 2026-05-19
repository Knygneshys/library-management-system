using backend.Dtos.Book;

namespace backend.Services.Interfaces;

public interface IBookServices
{
    Task<List<BookDto>> GetBooksAsync();
}