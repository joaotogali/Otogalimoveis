using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otogalimoveis.Infrastructure.Data
{
    public class AluguelData : IAluguelData
    {
        private readonly OtogaliDBContext _context;

        public AluguelData(OtogaliDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluguel>> GetAllAsync()
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .ToListAsync();
        }

        public async Task<Aluguel?> GetByIdAsync(int id)
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Aluguel aluguel)
        {
            await _context.Alugueis.AddAsync(aluguel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aluguel aluguel)
        {
            _context.Alugueis.Update(aluguel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel != null)
            {
                _context.Alugueis.Remove(aluguel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisAtivosAsync()
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .Where(a => a.DataFim == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorImovelAsync(int imovelId)
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .Where(a => a.ImovelId == imovelId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorLocatarioAsync(int locatarioId)
        {
            return await _context.Alugueis
                .Include(a => a.Imovel)
                .Include(a => a.Locatario)
                .Where(a => a.LocatarioId == locatarioId)
                .ToListAsync();
        }
    }
} 