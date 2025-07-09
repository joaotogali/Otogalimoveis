using Otogalimoveis.Domain.Model;

namespace Otogalimoveis.Application.Services
{
    public interface ILocatarioService
    {
        Task<IEnumerable<Locatario>> GetAllAsync();
        Task<Locatario?> GetByIdAsync(int id);
        Task<Locatario> CreateAsync(Locatario locatario);
        Task<Locatario> UpdateAsync(int id, Locatario locatario);
        Task<bool> DeleteAsync(int id);
        
        // Consultas específicas
        Task<IEnumerable<Locatario>> GetByCpfAsync(string cpf);
        Task<IEnumerable<Locatario>> GetByEmailAsync(string email);
        Task<IEnumerable<Locatario>> GetActiveLocatariosAsync();
        Task<IEnumerable<Aluguel>> GetRentalsByLocatarioAsync(int locatarioId);
        Task<bool> HasActiveRentalsAsync(int locatarioId);
    }
} 