namespace FlightBooking.Domain.Entities;

// Reference data - rarely changes, good candidate for caching at the service layer
public class Airport
{
    public int Id { get; set; }
    public string IataCode { get; set; } = string.Empty; // e.g. "KWI"
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public ICollection<Flight> DeparturesFromHere { get; set; } = new List<Flight>();
    public ICollection<Flight> ArrivalsToHere { get; set; } = new List<Flight>();
}
