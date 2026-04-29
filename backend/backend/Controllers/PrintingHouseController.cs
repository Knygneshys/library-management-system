using backend.Dtos.PrintingHouse;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrintingHouseController(IPrintingHouseServices printingHouseServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleCreateRequest([FromBody] PrintingHouseCreateDto dto)
    {
        try
        {
            var printingHouses = await printingHouseServices.Create(dto);

            return Ok(printingHouses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var printingHouses = await printingHouseServices.GetAll();

        return Ok(printingHouses);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> HandleUpdateRequest([FromRoute] Guid id, [FromBody] PrintingHouseUpdateDto dto)
    {
        try
        {
            var printingHouses = await printingHouseServices.Update(id, dto);

            return Ok(printingHouses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> HandleDeletionRequest([FromRoute] Guid id)
    {
        try
        {
            var printingHouses = await printingHouseServices.Delete(id);

            return Ok(printingHouses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}