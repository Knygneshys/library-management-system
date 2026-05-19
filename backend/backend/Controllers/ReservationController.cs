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

    [HttpGet("free-count/{bookId:guid}")]
    public async Task<IActionResult> GetFreeCopyCountAsync(Guid bookId)
    {
        try
        {
            var freeCount = await reservationServices.GetFreeCopyCountAsync(bookId);

            return Ok(freeCount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reserve/{bookId:guid}")]
    public async Task<IActionResult> ReserveCopyAsync(Guid bookId)
    {
        try
        {
            var result = await reservationServices.ReserveCopyAsync(bookId);

            if (!result)
            {
                return BadRequest("No free copies available.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("queue/{bookId:guid}")]
    public async Task<IActionResult> StayInQueueAsync(Guid bookId)
    {
        try
        {
            var result = await reservationServices.GoToQueueAsync(bookId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}