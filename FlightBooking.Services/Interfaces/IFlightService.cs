using FlightBooking.Services.DTOs;

namespace FlightBooking.Services.Interfaces;

public interface IFlightService
{
    Task<IReadOnlyList<FlightResultDto>> SearchAsync(FlightSearchRequestDto request, CancellationToken ct = default);
    Task<FlightResultDto?> GetByIdAsync(int flightId, CancellationToken ct = default);
}
