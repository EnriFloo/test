using AutoveloxTest.Models;
using AutoveloxTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoveloxTest.Controllers
{
    public class MappaController : Controller
    {
        private readonly MappaService _mappaService;

        public MappaController(MappaService mappaService)
        {
            _mappaService = mappaService;
        }

        // Mostra la pagina iniziale con tutti gli autovelox o con ricerca
        public async Task<IActionResult> Index(string? regione, string? provincia, string? comune)
        {
            List<Mappa> autovelox;
            if (!string.IsNullOrEmpty(regione) || !string.IsNullOrEmpty(provincia) || !string.IsNullOrEmpty(comune))
            {
                autovelox = await _mappaService.CercaAutoveloxAsync(regione, provincia, comune);
            }
            else
            {
                autovelox = await _mappaService.GetTuttiAutoveloxAsync();
            }

            ViewBag.Regione = regione;
            ViewBag.Provincia = provincia;
            ViewBag.Comune = comune;

            return View(autovelox);
        }

        // Dettaglio autovelox per id
        public async Task<IActionResult> Dettaglio(int id)
        {
            var autovelox = await _mappaService.GetDettaglioAutoveloxAsync(id);
            if (autovelox == null)
                return NotFound();
            return View(autovelox);
        }
    }
}
