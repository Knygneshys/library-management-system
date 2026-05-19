using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationServices reservationServices) : ControllerBase
{
    [HttpPut("return/{reservationId:guid}")]
    public async Task<IActionResult> ReturnBookAsync(Guid reservationId)
    {
        try
        {
            var result = await reservationServices.SetReservationAsReturningAsync(reservationId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}