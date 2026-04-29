using backend.Dtos.Locker;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LockerController(ILockerServices LockerServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateLocker([FromBody] LockerCreateDto dto)
    {
        try
        {
            var LockerId = await LockerServices.CreateAsync(dto);

            return Ok(LockerId);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLockers()
    {
        var Lockers = await LockerServices.GetAllAsync();
        
        return Ok(Lockers);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLocker(Guid id, [FromBody] LockerUpdateDto dto)
    {
        try
        {
            var Lockers = await LockerServices.UpdateAsync(id, dto);

            return Ok(Lockers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLocker([FromRoute] Guid id)
    {
        try
        {
            LockerServices.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}