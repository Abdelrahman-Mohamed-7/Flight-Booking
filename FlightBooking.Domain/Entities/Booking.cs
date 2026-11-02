using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string BookingReference { get; set; } = string.Empty; // e.g. "FB-8K3J2Q"

    public string CustomerId { get; set; } = string.Empty;
    public ApplicationUser Customer { get; set; } = null!;

    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;

    public SeatClass Class { get; set; }
    public DateTime BookingDateUtc { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "KWD";

    public ICollection<BookingPassenger> BookingPassengers { get; set; } = new List<BookingPassenger>();
}
