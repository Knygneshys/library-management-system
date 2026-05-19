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






    [HttpPost("submit-pin")]
    public async Task<IActionResult> SubmitPINInput([FromBody] LockerSubmitPINDto dto)
    {
        try
        {
            //  getLockerByCode()
            var locker = await lockerServices.GetLockerByCodeAsync(dto.PinCode);

            // [Locker exists]
            if (locker != null)
            {

                await OpenLocker(locker.Id);

                // success
                return Ok(new { success = true, lockerId = locker.Id });
            }

            // [else] -> error
            return BadRequest(new { success = false, message = "Neteisingas PIN kodas." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }


    private async Task OpenLocker(Guid lockerId)
    {
        await lockerServices.OpenLockerAsync(lockerId);
    }


    [HttpGet("{id:guid}/is-closed")]
    public async Task<IActionResult> IsLockerClosed([FromRoute] Guid id)
    {
        try
        {
            bool isClosed = await lockerServices.IsLockerClosedAsync(id);

            return Ok(new { closed = isClosed });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> HandleLockerClosed([FromRoute] Guid id)
    {
        try
        {
            await lockerServices.HandleLockerClosedAsync(id);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}