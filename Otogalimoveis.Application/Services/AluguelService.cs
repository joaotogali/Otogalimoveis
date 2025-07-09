using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otogalimoveis.Application.Services
{
    public class AluguelService : IAluguelService
    {
        private readonly IAluguelData _aluguelData;
        private readonly IImovelData _imovelData;
        private readonly ILocatarioData _locatarioData;

        public AluguelService(IAluguelData aluguelData, IImovelData imovelData, ILocatarioData locatarioData)
        {
            _aluguelData = aluguelData;
            _imovelData = imovelData;
            _locatarioData = locatarioData;
        }

        public async Task<IEnumerable<Aluguel>> GetAllAsync()
        {
            return await _aluguelData.GetAllAsync();
        }

        public async Task<Aluguel?> GetByIdAsync(int id)
        {
            return await _aluguelData.GetByIdAsync(id);
        }

        public async Task AddAsync(Aluguel aluguel)
        {
            if (aluguel.DataInicio < DateTime.Today)
                throw new System.ArgumentException("Data de início não pode ser anterior a hoje");

            if (aluguel.DataFim.HasValue && aluguel.DataFim <= aluguel.DataInicio)
                throw new System.ArgumentException("Data de fim deve ser posterior à data de início");

            var imovel = await _imovelData.GetByIdAsync(aluguel.ImovelId);
            if (imovel == null)
                throw new System.ArgumentException("Imóvel não encontrado");

            var locatario = await _locatarioData.GetByIdAsync(aluguel.LocatarioId);
            if (locatario == null)
                throw new System.ArgumentException("Locatário não encontrado");

            if (imovel.Status != ImovelStatus.Disponivel)
                throw new System.InvalidOperationException("Imóvel não está disponível para aluguel");

            var alugueisAtivos = await _aluguelData.GetAlugueisPorImovelAsync(aluguel.ImovelId);
            var temAluguelAtivo = alugueisAtivos.Any(a => a.DataFim == null);
            if (temAluguelAtivo)
                throw new System.InvalidOperationException("Imóvel já possui aluguel ativo");

            await _aluguelData.AddAsync(aluguel);

            imovel.Status = ImovelStatus.Alugado;
            await _imovelData.UpdateAsync(imovel);
        }

        public async Task UpdateAsync(Aluguel aluguel)
        {
            if (aluguel.DataInicio < DateTime.Today)
                throw new System.ArgumentException("Data de início não pode ser anterior a hoje");

            if (aluguel.DataFim.HasValue && aluguel.DataFim <= aluguel.DataInicio)
                throw new System.ArgumentException("Data de fim deve ser posterior à data de início");

            await _aluguelData.UpdateAsync(aluguel);
        }

        public async Task DeleteAsync(int id)
        {
            var aluguel = await _aluguelData.GetByIdAsync(id);
            if (aluguel == null)
                throw new System.ArgumentException("Aluguel não encontrado");

            await _aluguelData.DeleteAsync(id);

            // Se o aluguel estava ativo, libera o imóvel
            if (aluguel.DataFim == null)
            {
                var imovel = await _imovelData.GetByIdAsync(aluguel.ImovelId);
                if (imovel != null)
                {
                    imovel.Status = ImovelStatus.Disponivel;
                    await _imovelData.UpdateAsync(imovel);
                }
            }
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisAtivosAsync()
        {
            return await _aluguelData.GetAlugueisAtivosAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorImovelAsync(int imovelId)
        {
            return await _aluguelData.GetAlugueisPorImovelAsync(imovelId);
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorLocatarioAsync(int locatarioId)
        {
            return await _aluguelData.GetAlugueisPorLocatarioAsync(locatarioId);
        }

        public async Task<int> CalcularTempoAluguelEmDiasAsync(int aluguelId)
        {
            var aluguel = await _aluguelData.GetByIdAsync(aluguelId);
            if (aluguel == null)
                throw new System.ArgumentException("Aluguel não encontrado");

            var dataFim = aluguel.DataFim ?? DateTime.Today;
            var tempoAluguel = dataFim - aluguel.DataInicio;
            
            return (int)tempoAluguel.TotalDays;
        }

        public async Task FinalizarAluguelAsync(int aluguelId)
        {
            var aluguel = await _aluguelData.GetByIdAsync(aluguelId);
            if (aluguel == null)
                throw new System.ArgumentException("Aluguel não encontrado");

            if (aluguel.DataFim.HasValue)
                throw new System.InvalidOperationException("Aluguel já foi finalizado");

            aluguel.DataFim = DateTime.Today;
            await _aluguelData.UpdateAsync(aluguel);

            var imovel = await _imovelData.GetByIdAsync(aluguel.ImovelId);
            if (imovel != null)
            {
                imovel.Status = ImovelStatus.Disponivel;
                await _imovelData.UpdateAsync(imovel);
            }
        }
    }
} 