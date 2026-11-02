namespace FlightBooking.MVC.Employee.Models;

public class DashboardViewModel
{
    public List<AirportViewModel> Airports { get; set; } = new();
    public string? From { get; set; }
    public string? To { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public List<FlightRowViewModel>? Flights { get; set; }
}
