using FlightBooking.Domain.Enums;

namespace FlightBooking.Services.DTOs;

public class CreateBookingDto
{
    public string CustomerId { get; set; } = string.Empty;
    public int FlightId { get; set; }
    public SeatClass Class { get; set; }
    public List<PassengerDto> Passengers { get; set; } = new();
}

public class PassengerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; } = string.Empty;
}
