namespace FlightBooking.Domain.Entities;

public class Passenger
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; } = string.Empty;

    public ICollection<BookingPassenger> BookingPassengers { get; set; } = new List<BookingPassenger>();
}
