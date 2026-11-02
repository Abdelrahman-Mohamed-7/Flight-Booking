using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class FlightPricingConfiguration : IEntityTypeConfiguration<FlightPricing>
{
    public void Configure(EntityTypeBuilder<FlightPricing> builder)
    {
        builder.ToTable("FlightPricings");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price).HasColumnType("decimal(10,3)"); // KWD uses 3 decimal places
        builder.Property(p => p.Currency).IsRequired().HasMaxLength(3);

        builder.HasIndex(p => new { p.FlightId, p.Class }).IsUnique();
    }
}
