using Otogalimoveis.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Domain.Data
{
    public interface ILocatarioData 
    {
        Task<IEnumerable<Locatario>> GetAllAsync();
        Task<Locatario> GetByIdAsync(int id);
        Task AddAsync(Locatario locatario);
        Task UpdateAsync(Locatario locatario);
        Task DeleteAsync(int id);
    }
} 