using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingReference).IsRequired().HasMaxLength(10);
        builder.Property(b => b.TotalPrice).HasColumnType("decimal(10,3)");
        builder.Property(b => b.Currency).IsRequired().HasMaxLength(3);

        builder.HasIndex(b => b.BookingReference).IsUnique();

        // A customer's "my bookings" page filters by CustomerId + Status
        builder.HasIndex(b => new { b.CustomerId, b.Status });

        builder.HasOne(b => b.Customer)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Flight)
            .WithMany(f => f.Bookings)
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
