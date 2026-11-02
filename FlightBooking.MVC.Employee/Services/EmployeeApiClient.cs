using System.Net.Http.Json;
using FlightBooking.MVC.Employee.Models;

namespace FlightBooking.MVC.Employee.Services;

// Typed client for the employee-facing calls against FlightBooking.API.
// Same API, different surface: employees see all flights + can search by route/date.
public class EmployeeApiClient
{
    private readonly HttpClient _http;

    public EmployeeApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AirportViewModel>> GetAirportsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<AirportViewModel>>("api/airports", ct);
        return result ?? new List<AirportViewModel>();
    }

    public async Task<List<FlightRowViewModel>> SearchFlightsAsync(string from, string to, DateTime date, CancellationToken ct = default)
    {
        var url = $"api/flights/search?from={from}&to={to}&date={date:yyyy-MM-dd}&pageSize=50";
        var result = await _http.GetFromJsonAsync<List<FlightRowViewModel>>(url, ct);
        return result ?? new List<FlightRowViewModel>();
    }
}
