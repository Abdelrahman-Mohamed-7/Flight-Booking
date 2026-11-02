using FlightBooking.MVC.Employee.Models;
using FlightBooking.MVC.Employee.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.MVC.Employee.Controllers;

// This app is the employee-facing side: flight/inventory oversight.
// In production every action here would carry [Authorize(Policy = "EmployeeOnly")]
// once the JWT from FlightBooking.API's /api/auth/login is wired into this app's auth handler.
public class DashboardController : Controller
{
    private readonly EmployeeApiClient _api;

    public DashboardController(EmployeeApiClient api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var vm = new DashboardViewModel
        {
            Airports = await _api.GetAirportsAsync(ct),
            Date = DateTime.Today
        };
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string from, string to, DateTime date, CancellationToken ct)
    {
        var vm = new DashboardViewModel
        {
            Airports = await _api.GetAirportsAsync(ct),
            From = from,
            To = to,
            Date = date,
            Flights = await _api.SearchFlightsAsync(from, to, date, ct)
        };
        return View("Index", vm);
    }

    public IActionResult Error() => View();
}
