using FlightBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController : ControllerBase
{
    private readonly IAirportService _airportService;

    public AirportsController(IAirportService airportService)
    {
        _airportService = airportService;
    }

    // Anonymous: both the employee and customer MVC apps need this for search dropdowns
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var airports = await _airportService.GetAllAsync(ct);
        return Ok(airports.Select(a => new { a.Id, a.IataCode, a.Name, a.City, a.Country }));
    }
}
