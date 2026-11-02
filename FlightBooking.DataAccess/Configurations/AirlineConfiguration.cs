using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> builder)
    {
        builder.ToTable("Airlines");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Code).IsRequired().HasMaxLength(5);
        builder.Property(a => a.LogoUrl).HasMaxLength(500);

        builder.HasIndex(a => a.Code).IsUnique();

        builder.HasMany(a => a.Flights)
            .WithOne(f => f.Airline)
            .HasForeignKey(f => f.AirlineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
