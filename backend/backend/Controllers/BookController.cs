using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IBookServices bookServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync()
    {
        var books = await bookServices.GetBooksAsync();

        return Ok(books);
    }
}