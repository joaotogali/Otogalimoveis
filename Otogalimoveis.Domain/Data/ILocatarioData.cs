using Otogalimoveis.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Domain.Data
{
    public interface ILocatarioData 
    {
        Task<IEnumerable<Locatario>> GetAllAsync();
        Task<Locatario?> GetByIdAsync(int id);
        Task<Locatario> CreateAsync(Locatario locatario);
        Task<Locatario> UpdateAsync(Locatario locatario);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Locatario>> GetByCpfAsync(string cpf);
        Task<IEnumerable<Locatario>> GetByEmailAsync(string email);
    }
} 