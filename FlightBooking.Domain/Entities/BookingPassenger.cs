namespace FlightBooking.Domain.Entities;

// Join entity: which passengers travel on a given booking, with their seat/ticket
public class BookingPassenger
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int PassengerId { get; set; }
    public Passenger Passenger { get; set; } = null!;

    public string? SeatNumber { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
}
