using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.DataAccess.Seed;

// Fluent API seeding via HasData. Everything here must be fixed/static:
// no DateTime.Now, no Guid.NewGuid() at call time - EF snapshots these values into a migration.
public static class SeedData
{
    // Fixed reference date so migrations are reproducible
    private static readonly DateTime BaseDate = new(2026, 10, 21, 0, 0, 0, DateTimeKind.Utc);

    public static void Seed(ModelBuilder builder)
    {
        SeedRoles(builder);
        SeedAirports(builder);
        SeedAirlines(builder);
        SeedFlights(builder);
        SeedFlightPricings(builder);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "role-employee", Name = "Employee", NormalizedName = "EMPLOYEE" },
            new IdentityRole { Id = "role-customer", Name = "Customer", NormalizedName = "CUSTOMER" }
        );
    }

    private static void SeedAirports(ModelBuilder builder)
    {
        builder.Entity<Airport>().HasData(
            new Airport { Id = 1, IataCode = "KWI", Name = "Kuwait International Airport", City = "Kuwait City", Country = "Kuwait" },
            new Airport { Id = 2, IataCode = "IST", Name = "Istanbul Airport", City = "Istanbul", Country = "Turkey" },
            new Airport { Id = 3, IataCode = "DXB", Name = "Dubai International Airport", City = "Dubai", Country = "United Arab Emirates" },
            new Airport { Id = 4, IataCode = "DOH", Name = "Hamad International Airport", City = "Doha", Country = "Qatar" }
        );
    }

    private static void SeedAirlines(ModelBuilder builder)
    {
        builder.Entity<Airline>().HasData(
            new Airline { Id = 1, Name = "Turkish Airlines", Code = "TK" },
            new Airline { Id = 2, Name = "Emirates", Code = "EK" },
            new Airline { Id = 3, Name = "Qatar Airways", Code = "QR" }
        );
    }

    private static void SeedFlights(ModelBuilder builder)
    {
        builder.Entity<Flight>().HasData(
            // Kuwait -> Istanbul, Turkish Airlines, 1 stop (matches the reference screenshots)
            new Flight
            {
                Id = 1,
                FlightNumber = "TK772",
                AirlineId = 1,
                DepartureAirportId = 1,
                ArrivalAirportId = 2,
                DepartureTimeUtc = BaseDate.AddHours(21).AddMinutes(0),
                ArrivalTimeUtc = BaseDate.AddHours(23).AddMinutes(50),
                DurationMinutes = 230,
                StopsCount = 1,
                AircraftType = "Airbus A321",
                IsActive = true
            },
            // Kuwait -> Dubai, Emirates, direct
            new Flight
            {
                Id = 2,
                FlightNumber = "EK853",
                AirlineId = 2,
                DepartureAirportId = 1,
                ArrivalAirportId = 3,
                DepartureTimeUtc = BaseDate.AddHours(21).AddMinutes(20),
                ArrivalTimeUtc = BaseDate.AddHours(23).AddMinutes(15),
                DurationMinutes = 115,
                StopsCount = 0,
                AircraftType = "Boeing 777",
                IsActive = true
            },
            // Kuwait -> Doha, Qatar Airways, direct
            new Flight
            {
                Id = 3,
                FlightNumber = "QR1032",
                AirlineId = 3,
                DepartureAirportId = 1,
                ArrivalAirportId = 4,
                DepartureTimeUtc = BaseDate.AddHours(21).AddMinutes(0),
                ArrivalTimeUtc = BaseDate.AddHours(23).AddMinutes(5),
                DurationMinutes = 125,
                StopsCount = 0,
                AircraftType = "Airbus A320",
                IsActive = true
            }
        );
    }

    private static void SeedFlightPricings(ModelBuilder builder)
    {
        builder.Entity<FlightPricing>().HasData(
            // Flight 1: Kuwait -> Istanbul, 220 KWD economy
            new FlightPricing { Id = 1, FlightId = 1, Class = SeatClass.Economy, Price = 220.000m, Currency = "KWD", TotalSeats = 150, AvailableSeats = 3 },
            new FlightPricing { Id = 2, FlightId = 1, Class = SeatClass.Business, Price = 520.000m, Currency = "KWD", TotalSeats = 20, AvailableSeats = 8 },

            // Flight 2: Kuwait -> Dubai, 250 KWD economy
            new FlightPricing { Id = 3, FlightId = 2, Class = SeatClass.Economy, Price = 250.000m, Currency = "KWD", TotalSeats = 180, AvailableSeats = 2 },
            new FlightPricing { Id = 4, FlightId = 2, Class = SeatClass.Business, Price = 610.000m, Currency = "KWD", TotalSeats = 24, AvailableSeats = 10 },

            // Flight 3: Kuwait -> Doha, 280 KWD economy
            new FlightPricing { Id = 5, FlightId = 3, Class = SeatClass.Economy, Price = 280.000m, Currency = "KWD", TotalSeats = 140, AvailableSeats = 5 },
            new FlightPricing { Id = 6, FlightId = 3, Class = SeatClass.Business, Price = 640.000m, Currency = "KWD", TotalSeats = 16, AvailableSeats = 6 }
        );
    }
}
