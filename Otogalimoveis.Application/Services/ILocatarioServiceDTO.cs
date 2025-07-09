using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.Application.Services
{
    public interface ILocatarioServiceDTO
    {
        Task<IEnumerable<LocatarioDTO>> GetAllAsync();
        Task<LocatarioDetailDTO?> GetByIdAsync(int id);
        Task<LocatarioDTO> CreateAsync(CreateLocatarioDTO createDto);
        Task<LocatarioDTO> UpdateAsync(int id, UpdateLocatarioDTO updateDto);
        Task<bool> DeleteAsync(int id);
        
        // Consultas específicas
        Task<IEnumerable<LocatarioDTO>> GetByCpfAsync(string cpf);
        Task<IEnumerable<LocatarioDTO>> GetByEmailAsync(string email);
        Task<IEnumerable<LocatarioDTO>> GetActiveLocatariosAsync();
        Task<IEnumerable<AluguelDTO>> GetRentalsByLocatarioAsync(int locatarioId);
        Task<bool> HasActiveRentalsAsync(int locatarioId);
    }
} 