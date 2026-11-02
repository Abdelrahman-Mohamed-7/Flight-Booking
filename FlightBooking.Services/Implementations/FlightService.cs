using FlightBooking.DataAccess;
using FlightBooking.Domain.Enums;
using FlightBooking.Services.DTOs;
using FlightBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Services.Implementations;

public class FlightService : IFlightService
{
    private readonly AppDbContext _db;

    public FlightService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<FlightResultDto>> SearchAsync(FlightSearchRequestDto request, CancellationToken ct = default)
    {
        var dayStart = request.DepartureDate.Date;
        var dayEnd = dayStart.AddDays(1);

        // AsNoTracking + Select projection: only the columns the UI needs travel over the wire,
        // and EF never builds change-tracked entity graphs for a read-only search result.
        var query = _db.Flights
            .AsNoTracking()
            .Where(f => f.IsActive
                && f.DepartureAirport.IataCode == request.DepartureAirportCode
                && f.ArrivalAirport.IataCode == request.ArrivalAirportCode
                && f.DepartureTimeUtc >= dayStart
                && f.DepartureTimeUtc < dayEnd)
            .OrderBy(f => f.DepartureTimeUtc)
            .Select(f => new FlightResultDto
            {
                FlightId = f.Id,
                FlightNumber = f.FlightNumber,
                AirlineName = f.Airline.Name,
                AirlineLogoUrl = f.Airline.LogoUrl,
                DepartureAirportCode = f.DepartureAirport.IataCode,
                DepartureCity = f.DepartureAirport.City,
                DepartureTimeUtc = f.DepartureTimeUtc,
                ArrivalAirportCode = f.ArrivalAirport.IataCode,
                ArrivalCity = f.ArrivalAirport.City,
                ArrivalTimeUtc = f.ArrivalTimeUtc,
                DurationMinutes = f.DurationMinutes,
                StopsCount = f.StopsCount,
                Currency = "KWD",
                EconomyPrice = f.Pricings.Where(p => p.Class == SeatClass.Economy).Select(p => p.Price).FirstOrDefault(),
                EconomyAvailableSeats = f.Pricings.Where(p => p.Class == SeatClass.Economy).Select(p => p.AvailableSeats).FirstOrDefault(),
                BusinessPrice = f.Pricings.Where(p => p.Class == SeatClass.Business).Select((FlightBooking.Domain.Entities.FlightPricing p) => (decimal?)p.Price).FirstOrDefault()
            });

        return await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);
    }

    public async Task<FlightResultDto?> GetByIdAsync(int flightId, CancellationToken ct = default)
    {
        return await _db.Flights
            .AsNoTracking()
            .Where(f => f.Id == flightId)
            .Select(f => new FlightResultDto
            {
                FlightId = f.Id,
                FlightNumber = f.FlightNumber,
                AirlineName = f.Airline.Name,
                AirlineLogoUrl = f.Airline.LogoUrl,
                DepartureAirportCode = f.DepartureAirport.IataCode,
                DepartureCity = f.DepartureAirport.City,
                DepartureTimeUtc = f.DepartureTimeUtc,
                ArrivalAirportCode = f.ArrivalAirport.IataCode,
                ArrivalCity = f.ArrivalAirport.City,
                ArrivalTimeUtc = f.ArrivalTimeUtc,
                DurationMinutes = f.DurationMinutes,
                StopsCount = f.StopsCount,
                Currency = "KWD",
                EconomyPrice = f.Pricings.Where(p => p.Class == SeatClass.Economy).Select(p => p.Price).FirstOrDefault(),
                EconomyAvailableSeats = f.Pricings.Where(p => p.Class == SeatClass.Economy).Select(p => p.AvailableSeats).FirstOrDefault(),
                BusinessPrice = f.Pricings.Where(p => p.Class == SeatClass.Business).Select((FlightBooking.Domain.Entities.FlightPricing p) => (decimal?)p.Price).FirstOrDefault()
            })
            .FirstOrDefaultAsync(ct);
    }
}
