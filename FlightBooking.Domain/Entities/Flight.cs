namespace FlightBooking.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;

    public int AirlineId { get; set; }
    public Airline Airline { get; set; } = null!;

    public int DepartureAirportId { get; set; }
    public Airport DepartureAirport { get; set; } = null!;

    public int ArrivalAirportId { get; set; }
    public Airport ArrivalAirport { get; set; } = null!;

    public DateTime DepartureTimeUtc { get; set; }
    public DateTime ArrivalTimeUtc { get; set; }
    public int DurationMinutes { get; set; }
    public int StopsCount { get; set; }
    public string AircraftType { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<FlightPricing> Pricings { get; set; } = new List<FlightPricing>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
