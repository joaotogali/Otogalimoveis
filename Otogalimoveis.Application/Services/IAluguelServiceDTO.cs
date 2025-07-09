using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.Application.Services
{
    public interface IAluguelServiceDTO
    {
        Task<IEnumerable<AluguelDTO>> GetAllAsync();
        Task<AluguelDetailDTO?> GetByIdAsync(int id);
        Task<AluguelDTO> CreateAsync(CreateAluguelDTO createDto);
        Task<AluguelDTO> UpdateAsync(int id, UpdateAluguelDTO updateDto);
        Task<bool> DeleteAsync(int id);
        
        // Consultas específicas
        Task<IEnumerable<AluguelDTO>> GetAlugueisAtivosAsync();
        Task<IEnumerable<AluguelDTO>> GetAlugueisPorImovelAsync(int imovelId);
        Task<IEnumerable<AluguelDTO>> GetAlugueisPorLocatarioAsync(int locatarioId);
        Task<int> CalcularTempoAluguelEmDiasAsync(int aluguelId);
        Task FinalizarAluguelAsync(int aluguelId);
    }
} 