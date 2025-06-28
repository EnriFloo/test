using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoveloxProject.Models;

[Authorize]
public class DashboardController : Controller
{
    private readonly AutoveloxContext _context;

    public DashboardController(AutoveloxContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Numero totale autovelox in Italia
        int totaleAutovelox = await _context.Mappa.CountAsync();

        // Numero autovelox per ogni regione (elenco)
        var perRegione = await _context.Mappa
            .Include(m => m.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                    .ThenInclude(p => p.IdRegioneNavigation)
            .GroupBy(m => m.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione)
            .Select(g => new RegioneAutoveloxViewModel
            {
                Regione = g.Key,
                NumeroAutovelox = g.Count()
            })
            .OrderBy(x => x.Regione)
            .ToListAsync();

        // Passa i dati a una viewmodel
        var model = new DashboardViewModel
        {
            TotaleAutovelox = totaleAutovelox,
            AutoveloxPerRegione = perRegione
        };

        return View(model);
    }
}

// ViewModel per i dati della dashboard:
public class DashboardViewModel
{
    public int TotaleAutovelox { get; set; }
    public List<RegioneAutoveloxViewModel> AutoveloxPerRegione { get; set; }
}

public class RegioneAutoveloxViewModel
{
    public string Regione { get; set; }
    public int NumeroAutovelox { get; set; }
}

