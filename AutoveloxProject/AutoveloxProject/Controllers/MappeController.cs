using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoveloxProject.Models;

public class MappeController : Controller
{
    private readonly AutoveloxContext _context;

    public MappeController(AutoveloxContext context)
    {
        _context = context;
    }

    // Elenco di tutti gli autovelox
    public async Task<IActionResult> Index()
    {
        var autovelox = await _context.Mappa
            .Include(m => m.IdComuneNavigation)
            .ThenInclude(c => c.IdProvinciaNavigation)
            .ThenInclude(p => p.IdRegioneNavigation)
            .ToListAsync();

        return View(autovelox);
    }

    // Ricerca parametrica: regione, provincia o comune
    public async Task<IActionResult> Ricerca(string tipo, string valore)
    {
        IQueryable<Mappa> query = _context.Mappa
            .Include(m => m.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                    .ThenInclude(p => p.IdRegioneNavigation);

        if (!string.IsNullOrEmpty(tipo) && !string.IsNullOrEmpty(valore))
        {
            switch (tipo.ToLower())
            {
                case "regione":
                    query = query.Where(m => m.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione == valore);
                    break;
                case "provincia":
                    query = query.Where(m => m.IdComuneNavigation.IdProvinciaNavigation.Denominazione == valore);
                    break;
                case "comune":
                    query = query.Where(m => m.IdComuneNavigation.Denominazione == valore);
                    break;
            }
        }

        var autovelox = await query.ToListAsync();
        return View("Index", autovelox);
    }

    // Dettaglio autovelox per Id
    public async Task<IActionResult> Dettaglio(int id)
    {
        var autovelox = await _context.Mappa
            .Include(m => m.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                    .ThenInclude(p => p.IdRegioneNavigation)
                        .ThenInclude(r => r.IdRipartizioneGeograficaNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (autovelox == null)
            return NotFound();

        return View(autovelox);
    }
}
