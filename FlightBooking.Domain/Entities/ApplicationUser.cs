using Microsoft.AspNetCore.Identity;

namespace FlightBooking.Domain.Entities;

// Shared Identity user for both the Employee and Customer MVC apps; role decides which app/UI they get
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
