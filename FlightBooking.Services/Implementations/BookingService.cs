using FlightBooking.DataAccess;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using FlightBooking.Services.DTOs;
using FlightBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly AppDbContext _db;

    public BookingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<BookingResultDto> CreateBookingAsync(CreateBookingDto dto, CancellationToken ct = default)
    {
        if (dto.Passengers.Count == 0)
            throw new InvalidOperationException("At least one passenger is required.");

        // Everything below runs in one transaction: seat count check + decrement + booking insert
        // must succeed or fail together, otherwise concurrent bookings could oversell a flight.
        await using var transaction = await _db.Database.BeginTransactionAsync(ct);

        var pricing = await _db.FlightPricings
            .SingleAsync(p => p.FlightId == dto.FlightId && p.Class == dto.Class, ct);

        if (pricing.AvailableSeats < dto.Passengers.Count)
            throw new InvalidOperationException("Not enough seats available in the selected class.");

        pricing.AvailableSeats -= dto.Passengers.Count;

        var flight = await _db.Flights.AsNoTracking().SingleAsync(f => f.Id == dto.FlightId, ct);

        var booking = new Booking
        {
            BookingReference = GenerateReference(),
            CustomerId = dto.CustomerId,
            FlightId = dto.FlightId,
            Class = dto.Class,
            BookingDateUtc = DateTime.UtcNow,
            Status = BookingStatus.Confirmed,
            TotalPrice = pricing.Price * dto.Passengers.Count,
            Currency = pricing.Currency
        };

        foreach (var p in dto.Passengers)
        {
            var passenger = new Passenger
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PassportNumber = p.PassportNumber,
                DateOfBirth = p.DateOfBirth,
                Nationality = p.Nationality
            };

            booking.BookingPassengers.Add(new BookingPassenger
            {
                Passenger = passenger,
                TicketNumber = GenerateTicketNumber()
            });
        }

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return new BookingResultDto
        {
            BookingId = booking.Id,
            BookingReference = booking.BookingReference,
            Status = booking.Status,
            TotalPrice = booking.TotalPrice,
            Currency = booking.Currency,
            FlightNumber = flight.FlightNumber,
            DepartureTimeUtc = flight.DepartureTimeUtc,
            PassengerCount = dto.Passengers.Count
        };
    }

    public async Task<IReadOnlyList<BookingResultDto>> GetBookingsForCustomerAsync(string customerId, CancellationToken ct = default)
    {
        return await _db.Bookings
            .AsNoTracking()
            .Where(b => b.CustomerId == customerId)
            .OrderByDescending(b => b.BookingDateUtc)
            .Select(b => new BookingResultDto
            {
                BookingId = b.Id,
                BookingReference = b.BookingReference,
                Status = b.Status,
                TotalPrice = b.TotalPrice,
                Currency = b.Currency,
                FlightNumber = b.Flight.FlightNumber,
                DepartureTimeUtc = b.Flight.DepartureTimeUtc,
                PassengerCount = b.BookingPassengers.Count
            })
            .ToListAsync(ct);
    }

    public async Task<bool> CancelBookingAsync(int bookingId, string customerId, CancellationToken ct = default)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(ct);

        var booking = await _db.Bookings
            .Include(b => b.BookingPassengers)
            .SingleOrDefaultAsync(b => b.Id == bookingId && b.CustomerId == customerId, ct);

        if (booking is null || booking.Status is BookingStatus.Cancelled or BookingStatus.Completed)
            return false;

        var pricing = await _db.FlightPricings
            .SingleAsync(p => p.FlightId == booking.FlightId && p.Class == booking.Class, ct);

        pricing.AvailableSeats += booking.BookingPassengers.Count;
        booking.Status = BookingStatus.Cancelled;

        await _db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return true;
    }

    private static string GenerateReference() => "FB-" + Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
    private static string GenerateTicketNumber() => "TKT-" + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
}
