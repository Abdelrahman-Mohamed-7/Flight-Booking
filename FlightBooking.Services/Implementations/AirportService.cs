using FlightBooking.DataAccess;
using FlightBooking.Domain.Entities;
using FlightBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FlightBooking.Services.Implementations;

public class AirportService : IAirportService
{
    private const string AllAirportsCacheKey = "airports:all";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(6);

    private readonly AppDbContext _db;
    private readonly IMemoryCache _cache;

    public AirportService(AppDbContext db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IReadOnlyList<Airport>> GetAllAsync(CancellationToken ct = default)
    {
        // Reference data changes rarely, so cache it instead of hitting SQL on every search page load
        if (_cache.TryGetValue(AllAirportsCacheKey, out IReadOnlyList<Airport>? cached) && cached is not null)
            return cached;

        var airports = await _db.Airports
            .AsNoTracking()
            .OrderBy(a => a.City)
            .ToListAsync(ct);

        _cache.Set(AllAirportsCacheKey, (IReadOnlyList<Airport>)airports, CacheDuration);
        return airports;
    }

    public async Task<Airport?> GetByCodeAsync(string iataCode, CancellationToken ct = default)
    {
        var all = await GetAllAsync(ct);
        return all.FirstOrDefault(a => a.IataCode.Equals(iataCode, StringComparison.OrdinalIgnoreCase));
    }
}
