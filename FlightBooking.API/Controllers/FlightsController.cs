using FlightBooking.Services.DTOs;
using FlightBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    // GET api/flights/search?from=KWI&to=IST&date=2026-10-21&adults=1
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] DateTime date,
        [FromQuery] int adults = 1,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var request = new FlightSearchRequestDto
        {
            DepartureAirportCode = from,
            ArrivalAirportCode = to,
            DepartureDate = date,
            Adults = adults,
            PageNumber = page,
            PageSize = Math.Min(pageSize, 50) // cap page size to protect the API from abuse
        };

        var results = await _flightService.SearchAsync(request, ct);
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var flight = await _flightService.GetByIdAsync(id, ct);
        return flight is null ? NotFound() : Ok(flight);
    }
}
