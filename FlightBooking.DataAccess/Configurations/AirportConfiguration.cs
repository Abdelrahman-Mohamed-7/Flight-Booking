using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("Airports");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.IataCode).IsRequired().HasMaxLength(3);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);

        // Unique + indexed: airport lookups by code happen on every search request
        builder.HasIndex(a => a.IataCode).IsUnique();

        builder.HasMany(a => a.DeparturesFromHere)
            .WithOne(f => f.DepartureAirport)
            .HasForeignKey(f => f.DepartureAirportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.ArrivalsToHere)
            .WithOne(f => f.ArrivalAirport)
            .HasForeignKey(f => f.ArrivalAirportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
