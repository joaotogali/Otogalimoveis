using Otogalimoveis.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Domain.Data
{
    public interface IAluguelData 
    {
        Task<IEnumerable<Aluguel>> GetAllAsync();
        Task<Aluguel?> GetByIdAsync(int id);
        Task AddAsync(Aluguel aluguel);
        Task UpdateAsync(Aluguel aluguel);
        Task DeleteAsync(int id);
        Task<IEnumerable<Aluguel>> GetAlugueisAtivosAsync();
        Task<IEnumerable<Aluguel>> GetAlugueisPorImovelAsync(int imovelId);
        Task<IEnumerable<Aluguel>> GetAlugueisPorLocatarioAsync(int locatarioId);
    }
} 