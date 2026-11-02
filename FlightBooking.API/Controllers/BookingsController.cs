using System.Security.Claims;
using FlightBooking.Services.DTOs;
using FlightBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // must be authenticated; either role can hit these (employee books on a customer's behalf too)
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto, CancellationToken ct)
    {
        try
        {
            var result = await _bookingService.CreateBookingAsync(dto, ct);
            return CreatedAtAction(nameof(GetMine), new { }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message }); // e.g. not enough seats
        }
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetMine(CancellationToken ct)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var bookings = await _bookingService.GetBookingsForCustomerAsync(customerId, ct);
        return Ok(bookings);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var cancelled = await _bookingService.CancelBookingAsync(id, customerId, ct);
        return cancelled ? NoContent() : NotFound();
    }
}
