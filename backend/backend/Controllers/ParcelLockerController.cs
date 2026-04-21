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
}