using Microsoft.EntityFrameworkCore;
using Otogalimoveis.Domain.Data;
using Otogalimoveis.Domain.Model;

namespace Otogalimoveis.Infrastructure.Data
{
    public class ImovelData : IImovelData
    {
        private readonly OtogaliDBContext _context;

        public ImovelData(OtogaliDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
        {
            return await _context.Imoveis.ToListAsync();
        }

        public async Task<Imovel?> GetByIdAsync(int id)
        {
            return await _context.Imoveis.FindAsync(id);
        }

        public async Task AddAsync(Imovel imovel)
        {
            await _context.Imoveis.AddAsync(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            _context.Imoveis.Update(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
                await _context.SaveChangesAsync();
            }
        }
    }
} 