using backend.Dtos.Locker;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LockerController(ILockerServices lockerServices) : ControllerBase
{
    [HttpPost("{parelLockerId:guid}")]
    public async Task<IActionResult> CreateLocker([FromRoute] Guid parelLockerId, [FromBody] LockerCreateDto dto)
    {
        try
        {
            var lockerId = await lockerServices.CreateAsync(parelLockerId, dto);

            return Ok(lockerId);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLockers()
    {
        var Lockers = await lockerServices.GetAllAsync();
        
        return Ok(Lockers);
    }

    [HttpGet("{parcelLockerId:guid}")]
    public async Task<IActionResult> GetLockersByParcelLocker(Guid parcelLockerId)
    {
        var Lockers = await lockerServices.GetLockersByParcelLockerAsync(parcelLockerId);
        
        return Ok(Lockers);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLocker(Guid id, [FromBody] LockerUpdateDto dto)
    {
        try
        {
            var Lockers = await lockerServices.UpdateAsync(id, dto);

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
            lockerServices.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}