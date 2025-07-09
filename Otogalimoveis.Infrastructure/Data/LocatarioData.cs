using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<Locatario?> GetByIdAsync(int id)
        {
            return await _context.Locatarios.FindAsync(id);
        }

        public async Task<Locatario> CreateAsync(Locatario locatario)
        {
            await _context.Locatarios.AddAsync(locatario);
            await _context.SaveChangesAsync();
            return locatario;
        }

        public async Task<Locatario> UpdateAsync(Locatario locatario)
        {
            _context.Locatarios.Update(locatario);
            await _context.SaveChangesAsync();
            return locatario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var locatario = await _context.Locatarios.FindAsync(id);
            if (locatario != null)
            {
                _context.Locatarios.Remove(locatario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Locatario>> GetByCpfAsync(string cpf)
        {
            return await _context.Locatarios
                .Where(l => l.Cpf == cpf)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locatario>> GetByEmailAsync(string email)
        {
            return await _context.Locatarios
                .Where(l => l.Email == email)
                .ToListAsync();
        }
    }
} 