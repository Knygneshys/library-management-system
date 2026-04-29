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
        var parcelLocker = await parcelLockerServices.GetAllAsync();
        
        return Ok(parcelLocker);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateParcelLocker(Guid id, [FromBody] ParcelLockerUpdateDto dto)
    {
        try
        {
            var parcelLocker = await parcelLockerServices.UpdateAsync(id, dto);

            return Ok(parcelLocker);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteParcelLocker([FromRoute] Guid id)
    {
        try
        {
            parcelLockerServices.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}