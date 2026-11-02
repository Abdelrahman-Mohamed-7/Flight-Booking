namespace FlightBooking.MVC.Customer.Models;

public class SearchFormViewModel
{
    public List<AirportViewModel> Airports { get; set; } = new();
    public string? From { get; set; }
    public string? To { get; set; }
    public DateTime? Date { get; set; }
    public int Adults { get; set; } = 1;
    public List<FlightResultViewModel>? Results { get; set; }
}
