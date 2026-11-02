using System.Net.Http.Json;
using FlightBooking.MVC.Customer.Models;

namespace FlightBooking.MVC.Customer.Services;

// Thin typed client wrapping calls to FlightBooking.API - keeps HttpClient/JSON details
// out of controllers, and is the one place that would add auth-token forwarding, retries, etc.
public class FlightApiClient
{
    private readonly HttpClient _http;

    public FlightApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AirportViewModel>> GetAirportsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<AirportViewModel>>("api/airports", ct);
        return result ?? new List<AirportViewModel>();
    }

    public async Task<List<FlightResultViewModel>> SearchFlightsAsync(
        string from, string to, DateTime date, int adults, CancellationToken ct = default)
    {
        var url = $"api/flights/search?from={from}&to={to}&date={date:yyyy-MM-dd}&adults={adults}";
        var result = await _http.GetFromJsonAsync<List<FlightResultViewModel>>(url, ct);
        return result ?? new List<FlightResultViewModel>();
    }
}
