using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otogalimoveis.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelData _imovelData;
        private readonly IAluguelData _aluguelData;

        public ImovelService(IImovelData imovelData, IAluguelData aluguelData)
        {
            _imovelData = imovelData;
            _aluguelData = aluguelData;
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
        {
            return await _imovelData.GetAllAsync();
        }

        public async Task<Imovel> GetByIdAsync(int id)
        {
            return await _imovelData.GetByIdAsync(id);
        }

        public async Task AddAsync(Imovel imovel)
        {
            if (string.IsNullOrWhiteSpace(imovel.Tipo))
                throw new System.ArgumentException("Tipo do imóvel é obrigatório");

            if (string.IsNullOrWhiteSpace(imovel.Endereco))
                throw new System.ArgumentException("Endereço é obrigatório");

            if (imovel.ValorLocacao <= 0)
                throw new System.ArgumentException("Valor da locação deve ser maior que zero");

            imovel.Status = ImovelStatus.Disponivel;

            await _imovelData.AddAsync(imovel);
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            if (string.IsNullOrWhiteSpace(imovel.Tipo))
                throw new System.ArgumentException("Tipo do imóvel é obrigatório");

            if (string.IsNullOrWhiteSpace(imovel.Endereco))
                throw new System.ArgumentException("Endereço é obrigatório");

            if (imovel.ValorLocacao <= 0)
                throw new System.ArgumentException("Valor da locação deve ser maior que zero");

            await _imovelData.UpdateAsync(imovel);
        }

        public async Task DeleteAsync(int id)
        {
            var alugueisAtivos = await _aluguelData.GetAlugueisPorImovelAsync(id);
            var temAlugueisAtivos = alugueisAtivos.Any(a => a.DataFim == null);

            if (temAlugueisAtivos)
                throw new System.InvalidOperationException("Não é possível excluir um imóvel com aluguéis ativos");

            await _imovelData.DeleteAsync(id);
        }

        public async Task<IEnumerable<Imovel>> GetImoveisDisponiveisAsync()
        {
            var imoveis = await _imovelData.GetAllAsync();
            return imoveis.Where(i => i.Status == ImovelStatus.Disponivel);
        }

        public async Task<IEnumerable<Imovel>> GetImoveisPorTipoAsync(string tipo)
        {
            var imoveis = await _imovelData.GetAllAsync();
            return imoveis.Where(i => i.Tipo.ToLower().Contains(tipo.ToLower()));
        }

        public async Task<IEnumerable<Imovel>> GetImoveisPorFaixaValorAsync(decimal valorMinimo, decimal valorMaximo)
        {
            var imoveis = await _imovelData.GetAllAsync();
            return imoveis.Where(i => i.ValorLocacao >= valorMinimo && i.ValorLocacao <= valorMaximo);
        }

        public async Task<bool> IsImovelDisponivelAsync(int imovelId)
        {
            var imovel = await _imovelData.GetByIdAsync(imovelId);
            if (imovel == null) return false;

            return imovel.Status == ImovelStatus.Disponivel;
        }
    }
} 