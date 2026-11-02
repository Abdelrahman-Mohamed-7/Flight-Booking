# Flight Booking Solution (.NET 8)

## Architecture
- **FlightBooking.Domain** — entities (Flight, Airport, Airline, Booking, Passenger, ApplicationUser) + enums
- **FlightBooking.DataAccess** — EF Core `AppDbContext`, Fluent API configurations (`Configurations/`), Fluent API `HasData` seed (`Seed/SeedData.cs`)
- **FlightBooking.Services** — DTOs + service interfaces/implementations (`IFlightService`, `IBookingService`, `IAirportService`)
- **FlightBooking.API** — the single Web API: JWT auth, Swagger, controllers for Auth/Airports/Flights/Bookings
- **FlightBooking.MVC.Employee** — MVC app for staff: flight/inventory dashboard, consumes the API over HttpClient
- **FlightBooking.MVC.Customer** — MVC app for customers: search + results + booking flow, consumes the API over HttpClient

## Performance choices baked in
- `AddDbContextPool` + `NoTracking` as the default query behavior (writes opt in to tracking explicitly)
- Search queries use `Select()` projections straight to DTOs — no over-fetching full entity graphs
- Composite index on `Flights(DepartureAirportId, ArrivalAirportId, DepartureTimeUtc, IsActive)` for the search path
- Reference data (airports) cached in `IMemoryCache` — not re-queried on every search page load
- Response compression + null-omitting JSON serialization on the API
- Seat booking wrapped in a DB transaction with a seat-count check to prevent overselling under concurrency

## Running it
1. Update the connection string in `FlightBooking.API/appsettings.json`.
2. `dotnet ef migrations add InitialCreate --project FlightBooking.DataAccess --startup-project FlightBooking.API`
3. `dotnet ef database update --project FlightBooking.DataAccess --startup-project FlightBooking.API`
4. Run `FlightBooking.API` first (note its port), then update `ApiBaseUrl` in both MVC apps' `appsettings.json` to match.
5. Run `FlightBooking.MVC.Customer` and `FlightBooking.MVC.Employee`.

## Auth
`POST /api/auth/register` with `role: "Customer"` or `"Employee"`, then `POST /api/auth/login` to get a JWT.
Wire the token into the MVC apps' auth (cookie exchange or forwarded bearer) as a next step — this scaffold focuses on the API/data/services layers and Fluent API seeding you asked for.
