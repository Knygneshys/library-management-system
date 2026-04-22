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
            var authors = await authorServices.CreateAsync(dto);

            return Ok(authors);
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id, [FromBody] AuthorUpdateDto dto)
    {
        try
        {
            var authors = await authorServices.UpdateAsync(id, dto);

            return Ok(authors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
    {
        try
        {
            var authors = await authorServices.DeleteAsync(id);

            return Ok(authors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}