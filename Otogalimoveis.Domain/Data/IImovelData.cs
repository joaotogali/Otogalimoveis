using Otogalimoveis.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Domain.Data
{
    public interface IImovelData 
    {
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task<Imovel?> GetByIdAsync(int id);
        Task AddAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(int id);
    }
}