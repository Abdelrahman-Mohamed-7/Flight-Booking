using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.DataAccess.Configurations;

public class BookingPassengerConfiguration : IEntityTypeConfiguration<BookingPassenger>
{
    public void Configure(EntityTypeBuilder<BookingPassenger> builder)
    {
        builder.ToTable("BookingPassengers");
        builder.HasKey(bp => bp.Id);

        builder.Property(bp => bp.TicketNumber).IsRequired().HasMaxLength(20);
        builder.Property(bp => bp.SeatNumber).HasMaxLength(5);

        builder.HasIndex(bp => bp.TicketNumber).IsUnique();

        builder.HasOne(bp => bp.Booking)
            .WithMany(b => b.BookingPassengers)
            .HasForeignKey(bp => bp.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bp => bp.Passenger)
            .WithMany(p => p.BookingPassengers)
            .HasForeignKey(bp => bp.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
