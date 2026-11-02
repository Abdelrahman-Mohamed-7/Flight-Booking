namespace FlightBooking.MVC.Customer.Models;

public class FlightResultViewModel
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string AirlineName { get; set; } = string.Empty;
    public string DepartureAirportCode { get; set; } = string.Empty;
    public string DepartureCity { get; set; } = string.Empty;
    public DateTime DepartureTimeUtc { get; set; }
    public string ArrivalAirportCode { get; set; } = string.Empty;
    public string ArrivalCity { get; set; } = string.Empty;
    public DateTime ArrivalTimeUtc { get; set; }
    public int DurationMinutes { get; set; }
    public int StopsCount { get; set; }
    public decimal EconomyPrice { get; set; }
    public int EconomyAvailableSeats { get; set; }
    public decimal? BusinessPrice { get; set; }
    public string Currency { get; set; } = "KWD";
}
