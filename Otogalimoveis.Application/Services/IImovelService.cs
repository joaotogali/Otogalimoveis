using Otogalimoveis.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.Application.Services
{
    public interface IImovelService
    {
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task<Imovel> GetByIdAsync(int id);
        Task AddAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(int id);
        Task<IEnumerable<Imovel>> GetImoveisDisponiveisAsync();
        Task<IEnumerable<Imovel>> GetImoveisPorTipoAsync(string tipo);
        Task<IEnumerable<Imovel>> GetImoveisPorFaixaValorAsync(decimal valorMinimo, decimal valorMaximo);
        Task<bool> IsImovelDisponivelAsync(int imovelId);
    }
} 