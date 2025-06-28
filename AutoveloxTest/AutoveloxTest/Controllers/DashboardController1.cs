using AutoveloxTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoveloxTest.Controllers
{
    [Authorize] // Accesso solo utenti autenticati
    public class DashboardController : Controller
    {
        private readonly MappaService _mappaService;

        public DashboardController(MappaService mappaService)
        {
            _mappaService = mappaService;
        }

        public async Task<IActionResult> Index()
        {
            var autovelox = await _mappaService.GetTuttiAutoveloxAsync();
            int totaleItalia = autovelox.Count;

            var perRegione = autovelox
                .GroupBy(x => x.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione)
                .Select(g => new DashboardItem
                {
                    Regione = g.Key,
                    Totale = g.Count()
                })
                .OrderBy(x => x.Regione)
                .ToList();

            ViewBag.TotaleItalia = totaleItalia;
            ViewBag.TotaliPerRegione = perRegione;

            return View();
        }
    }

    public class DashboardItem
    {
        public string Regione { get; set; }
        public int Totale { get; set; }
    }
}
