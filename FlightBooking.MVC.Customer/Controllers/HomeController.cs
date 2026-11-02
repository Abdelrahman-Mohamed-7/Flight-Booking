using FlightBooking.MVC.Customer.Models;
using FlightBooking.MVC.Customer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.MVC.Customer.Controllers;

public class HomeController : Controller
{
    private readonly FlightApiClient _api;

    public HomeController(FlightApiClient api)
    {
        _api = api;
    }

    // Landing page: airport list only, matches the reference "search" screenshot's dropdowns
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var vm = new SearchFormViewModel
        {
            Airports = await _api.GetAirportsAsync(ct),
            Date = DateTime.Today.AddDays(1),
            Adults = 1
        };
        return View(vm);
    }

    // Results page: matches the reference "flight list" screenshot's cards
    [HttpGet]
    public async Task<IActionResult> Results(string from, string to, DateTime date, int adults = 1, CancellationToken ct = default)
    {
        var vm = new SearchFormViewModel
        {
            Airports = await _api.GetAirportsAsync(ct),
            From = from,
            To = to,
            Date = date,
            Adults = adults,
            Results = await _api.SearchFlightsAsync(from, to, date, adults, ct)
        };
        return View(vm);
    }

    public IActionResult Error() => View();
}
