namespace FlightBooking.Services.DTOs;

public class FlightSearchRequestDto
{
    public string DepartureAirportCode { get; set; } = string.Empty;
    public string ArrivalAirportCode { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public int Adults { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
