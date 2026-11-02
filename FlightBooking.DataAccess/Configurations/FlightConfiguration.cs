using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("Flights");
        builder.HasKey(f => f.Id);

        builder.Property(f => f.FlightNumber).IsRequired().HasMaxLength(10);
        builder.Property(f => f.AircraftType).HasMaxLength(50);

        // The core search query filters by route + date + IsActive -> composite index
        builder.HasIndex(f => new { f.DepartureAirportId, f.ArrivalAirportId, f.DepartureTimeUtc, f.IsActive })
            .HasDatabaseName("IX_Flights_Search");

        builder.HasMany(f => f.Pricings)
            .WithOne(p => p.Flight)
            .HasForeignKey(p => p.FlightId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
