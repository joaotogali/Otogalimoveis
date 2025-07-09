using AutoMapper;
using Otogalimoveis.Domain.Data;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.Application.Services
{
    public class ImovelServiceDTO : IImovelServiceDTO
    {
        private readonly IImovelData _imovelData;
        private readonly IAluguelData _aluguelData;
        private readonly IMapper _mapper;

        public ImovelServiceDTO(IImovelData imovelData, IAluguelData aluguelData, IMapper mapper)
        {
            _imovelData = imovelData;
            _aluguelData = aluguelData;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImovelDTO>> GetAllAsync()
        {
            var imoveis = await _imovelData.GetAllAsync();
            return _mapper.Map<IEnumerable<ImovelDTO>>(imoveis);
        }

        public async Task<ImovelDetailDTO?> GetByIdAsync(int id)
        {
            var imovel = await _imovelData.GetByIdAsync(id);
            if (imovel == null) return null;

            var imovelDetail = _mapper.Map<ImovelDetailDTO>(imovel);
            
            // Carregar aluguéis relacionados
            var alugueis = await _aluguelData.GetAlugueisPorImovelAsync(id);
            imovelDetail.Alugueis = _mapper.Map<List<AluguelDTO>>(alugueis);

            return imovelDetail;
        }

        public async Task<ImovelDTO> CreateAsync(CreateImovelDTO createDto)
        {
            var imovel = _mapper.Map<Imovel>(createDto);
            
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(imovel.Tipo))
                throw new ArgumentException("Tipo do imóvel é obrigatório");

            if (string.IsNullOrWhiteSpace(imovel.Endereco))
                throw new ArgumentException("Endereço é obrigatório");

            if (imovel.ValorLocacao <= 0)
                throw new ArgumentException("Valor da locação deve ser maior que zero");

            await _imovelData.AddAsync(imovel);
            return _mapper.Map<ImovelDTO>(imovel);
        }

        public async Task<ImovelDTO> UpdateAsync(int id, UpdateImovelDTO updateDto)
        {
            var existingImovel = await _imovelData.GetByIdAsync(id);
            if (existingImovel == null)
                throw new InvalidOperationException("Imóvel não encontrado");

            // Validações de negócio
            if (string.IsNullOrWhiteSpace(updateDto.Tipo))
                throw new ArgumentException("Tipo do imóvel é obrigatório");

            if (string.IsNullOrWhiteSpace(updateDto.Endereco))
                throw new ArgumentException("Endereço é obrigatório");

            if (updateDto.ValorLocacao <= 0)
                throw new ArgumentException("Valor da locação deve ser maior que zero");

            _mapper.Map(updateDto, existingImovel);
            await _imovelData.UpdateAsync(existingImovel);
            
            return _mapper.Map<ImovelDTO>(existingImovel);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alugueisAtivos = await _aluguelData.GetAlugueisPorImovelAsync(id);
            var temAlugueisAtivos = alugueisAtivos.Any(a => a.DataFim == null);

            if (temAlugueisAtivos)
                throw new InvalidOperationException("Não é possível excluir um imóvel com aluguéis ativos");

            await _imovelData.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ImovelDTO>> GetImoveisDisponiveisAsync()
        {
            var imoveis = await _imovelData.GetAllAsync();
            var imoveisDisponiveis = imoveis.Where(i => i.Status == ImovelStatus.Disponivel);
            return _mapper.Map<IEnumerable<ImovelDTO>>(imoveisDisponiveis);
        }

        public async Task<IEnumerable<ImovelDTO>> GetImoveisPorTipoAsync(string tipo)
        {
            var imoveis = await _imovelData.GetAllAsync();
            var imoveisFiltrados = imoveis.Where(i => i.Tipo.ToLower().Contains(tipo.ToLower()));
            return _mapper.Map<IEnumerable<ImovelDTO>>(imoveisFiltrados);
        }

        public async Task<IEnumerable<ImovelDTO>> GetImoveisPorFaixaValorAsync(decimal valorMinimo, decimal valorMaximo)
        {
            var imoveis = await _imovelData.GetAllAsync();
            var imoveisFiltrados = imoveis.Where(i => i.ValorLocacao >= valorMinimo && i.ValorLocacao <= valorMaximo);
            return _mapper.Map<IEnumerable<ImovelDTO>>(imoveisFiltrados);
        }

        public async Task<bool> IsImovelDisponivelAsync(int imovelId)
        {
            var imovel = await _imovelData.GetByIdAsync(imovelId);
            if (imovel == null) return false;

            return imovel.Status == ImovelStatus.Disponivel;
        }
    }
} 