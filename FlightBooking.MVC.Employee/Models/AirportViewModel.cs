namespace FlightBooking.MVC.Employee.Models;

public class AirportViewModel
{
    public int Id { get; set; }
    public string IataCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
