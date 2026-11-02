using FlightBooking.Domain.Enums;

namespace FlightBooking.Services.DTOs;

public class BookingResultDto
{
    public int BookingId { get; set; }
    public string BookingReference { get; set; } = string.Empty;
    public BookingStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "KWD";
    public string FlightNumber { get; set; } = string.Empty;
    public DateTime DepartureTimeUtc { get; set; }
    public int PassengerCount { get; set; }
}
