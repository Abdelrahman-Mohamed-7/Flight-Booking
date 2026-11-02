using FlightBooking.DataAccess.Seed;
using FlightBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Airline> Airlines => Set<Airline>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<FlightPricing> FlightPricings => Set<FlightPricing>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingPassenger> BookingPassengers => Set<BookingPassenger>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply every IEntityTypeConfiguration<> in this assembly (Configurations folder)
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Fluent API seeding - static data only (HasData requires fixed keys/values, no DateTime.Now etc.)
        SeedData.Seed(builder);
    }
}
