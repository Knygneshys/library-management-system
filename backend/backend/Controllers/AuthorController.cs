using backend.Dtos.Author;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(IAuthorServices authorServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto dto)
    {
        try
        {
            var authorId = await authorServices.CreateAsync(dto);

            return Ok(authorId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await authorServices.GetAllAsync();
        
        return Ok(authors);
    }
}