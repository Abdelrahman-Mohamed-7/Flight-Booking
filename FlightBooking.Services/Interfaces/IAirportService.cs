using FlightBooking.Domain.Entities;

namespace FlightBooking.Services.Interfaces;

public interface IAirportService
{
    // Backed by IMemoryCache - airport list barely ever changes, no reason to hit the DB every call
    Task<IReadOnlyList<Airport>> GetAllAsync(CancellationToken ct = default);
    Task<Airport?> GetByCodeAsync(string iataCode, CancellationToken ct = default);
}
