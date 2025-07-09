using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.Application.Services
{
    public interface IImovelServiceDTO
    {
        Task<IEnumerable<ImovelDTO>> GetAllAsync();
        Task<ImovelDetailDTO?> GetByIdAsync(int id);
        Task<ImovelDTO> CreateAsync(CreateImovelDTO createDto);
        Task<ImovelDTO> UpdateAsync(int id, UpdateImovelDTO updateDto);
        Task<bool> DeleteAsync(int id);
        
        // Consultas específicas
        Task<IEnumerable<ImovelDTO>> GetImoveisDisponiveisAsync();
        Task<IEnumerable<ImovelDTO>> GetImoveisPorTipoAsync(string tipo);
        Task<IEnumerable<ImovelDTO>> GetImoveisPorFaixaValorAsync(decimal valorMinimo, decimal valorMaximo);
        Task<bool> IsImovelDisponivelAsync(int imovelId);
    }
} 