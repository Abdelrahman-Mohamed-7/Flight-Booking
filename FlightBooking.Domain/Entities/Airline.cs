namespace FlightBooking.Domain.Entities;

public class Airline
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // e.g. "QR", "TK"
    public string? LogoUrl { get; set; }

    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
