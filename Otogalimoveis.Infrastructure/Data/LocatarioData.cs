using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Infrastructure.Data
{
    public class LocatarioData : ILocatarioData
    {
        private readonly OtogaliDBContext _context;

        public LocatarioData(OtogaliDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locatario>> GetAllAsync()
        {
            return await _context.Locatarios.ToListAsync();
        }

        public async Task<Locatario> GetByIdAsync(int id)
        {
            return await _context.Locatarios.FindAsync(id);
        }

        public async Task AddAsync(Locatario locatario)
        {
            await _context.Locatarios.AddAsync(locatario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Locatario locatario)
        {
            _context.Locatarios.Update(locatario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var locatario = await _context.Locatarios.FindAsync(id);
            if (locatario != null)
            {
                _context.Locatarios.Remove(locatario);
                await _context.SaveChangesAsync();
            }
        }
    }
} 