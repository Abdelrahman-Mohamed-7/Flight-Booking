using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities;

// Price + inventory per seat class for a given flight
public class FlightPricing
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;

    public SeatClass Class { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "KWD";
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
}
