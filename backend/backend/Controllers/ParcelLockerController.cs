using backend.Dtos.ParcelLocker;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParcelLockerController(IParcelLockerServices parcelLockerServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateParcelLocker([FromBody] ParcelLockerCreateDto dto)
    {
        try
        {
            var parcelLockerId = await parcelLockerServices.CreateAsync(dto);

            return Ok(parcelLockerId);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParcelLockers()
    {
        var parcelLockers = await parcelLockerServices.GetAllAsync();
        
        return Ok(parcelLockers);
    }

    [HttpPut("{address}")]
    public async Task<IActionResult> UpdateParcelLocker(string address, [FromBody] ParcelLockerUpdateDto dto)
    {
        try
        {
            await parcelLockerServices.UpdateAsync(address, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteParcelLocker(Guid id)
    {
        try
        {
            await parcelLockerServices.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}