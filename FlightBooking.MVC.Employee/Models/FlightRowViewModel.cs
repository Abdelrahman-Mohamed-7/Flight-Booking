namespace FlightBooking.MVC.Employee.Models;

public class FlightRowViewModel
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string AirlineName { get; set; } = string.Empty;
    public string DepartureAirportCode { get; set; } = string.Empty;
    public DateTime DepartureTimeUtc { get; set; }
    public string ArrivalAirportCode { get; set; } = string.Empty;
    public DateTime ArrivalTimeUtc { get; set; }
    public decimal EconomyPrice { get; set; }
    public int EconomyAvailableSeats { get; set; }
}
