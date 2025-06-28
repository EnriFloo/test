using AutoveloxTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxTest.Services
{
    public class MappaService
    {
        private readonly AutoveloxDbContext _context;

        public MappaService(AutoveloxDbContext context)
        {
            _context = context;
        }

        // Recupera tutti gli autovelox in Italia
        public async Task<List<Mappa>> GetTuttiAutoveloxAsync()
        {
            return await _context.Mappas
                .Include(m => m.IdComuneNavigation)
                    .ThenInclude(c => c.IdProvinciaNavigation)
                        .ThenInclude(p => p.IdRegioneNavigation)
                .ToListAsync();
        }

        // Ricerca per regione, provincia, comune
        public async Task<List<Mappa>> CercaAutoveloxAsync(string? regione, string? provincia, string? comune)
        {
            var query = _context.Mappas
                .Include(m => m.IdComuneNavigation)
                    .ThenInclude(c => c.IdProvinciaNavigation)
                        .ThenInclude(p => p.IdRegioneNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(regione))
                query = query.Where(m => m.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione == regione);

            if (!string.IsNullOrEmpty(provincia))
                query = query.Where(m => m.IdComuneNavigation.IdProvinciaNavigation.Denominazione == provincia);

            if (!string.IsNullOrEmpty(comune))
                query = query.Where(m => m.IdComuneNavigation.Denominazione == comune);

            return await query.ToListAsync();
        }

        // Dettaglio autovelox per Id
        public async Task<Mappa?> GetDettaglioAutoveloxAsync(int id)
        {
            return await _context.Mappas
                .Include(m => m.IdComuneNavigation)
                    .ThenInclude(c => c.IdProvinciaNavigation)
                        .ThenInclude(p => p.IdRegioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
