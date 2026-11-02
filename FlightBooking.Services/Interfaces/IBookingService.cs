using FlightBooking.Services.DTOs;

namespace FlightBooking.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResultDto> CreateBookingAsync(CreateBookingDto dto, CancellationToken ct = default);
    Task<IReadOnlyList<BookingResultDto>> GetBookingsForCustomerAsync(string customerId, CancellationToken ct = default);
    Task<bool> CancelBookingAsync(int bookingId, string customerId, CancellationToken ct = default);
}
