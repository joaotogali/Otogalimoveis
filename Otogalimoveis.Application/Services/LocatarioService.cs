using Otogalimoveis.Domain.Data;
using Otogalimoveis.Domain.Model;

namespace Otogalimoveis.Application.Services
{
    public class LocatarioService : ILocatarioService
    {
        private readonly ILocatarioData _locatarioData;
        private readonly IAluguelData _aluguelData;

        public LocatarioService(ILocatarioData locatarioData, IAluguelData aluguelData)
        {
            _locatarioData = locatarioData;
            _aluguelData = aluguelData;
        }

        public async Task<IEnumerable<Locatario>> GetAllAsync()
        {
            return await _locatarioData.GetAllAsync();
        }

        public async Task<Locatario?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID do locatário deve ser maior que zero.");

            return await _locatarioData.GetByIdAsync(id);
        }

        public async Task<Locatario> CreateAsync(Locatario locatario)
        {
            ValidateLocatario(locatario);

            // Verificar se já existe um locatário com o mesmo CPF
            var existingCpf = await _locatarioData.GetByCpfAsync(locatario.Cpf);
            if (existingCpf.Any())
                throw new InvalidOperationException("Já existe um locatário cadastrado com este CPF.");

            // Verificar se já existe um locatário com o mesmo email
            var existingEmail = await _locatarioData.GetByEmailAsync(locatario.Email);
            if (existingEmail.Any())
                throw new InvalidOperationException("Já existe um locatário cadastrado com este email.");

            return await _locatarioData.CreateAsync(locatario);
        }

        public async Task<Locatario> UpdateAsync(int id, Locatario locatario)
        {
            if (id <= 0)
                throw new ArgumentException("ID do locatário deve ser maior que zero.");

            ValidateLocatario(locatario);

            var existingLocatario = await _locatarioData.GetByIdAsync(id);
            if (existingLocatario == null)
                throw new InvalidOperationException("Locatário não encontrado.");

            // Verificar se o CPF está sendo alterado e se já existe outro locatário com o mesmo CPF
            if (existingLocatario.Cpf != locatario.Cpf)
            {
                var existingCpf = await _locatarioData.GetByCpfAsync(locatario.Cpf);
                if (existingCpf.Any(l => l.Id != id))
                    throw new InvalidOperationException("Já existe outro locatário cadastrado com este CPF.");
            }

            // Verificar se o email está sendo alterado e se já existe outro locatário com o mesmo email
            if (existingLocatario.Email != locatario.Email)
            {
                var existingEmail = await _locatarioData.GetByEmailAsync(locatario.Email);
                if (existingEmail.Any(l => l.Id != id))
                    throw new InvalidOperationException("Já existe outro locatário cadastrado com este email.");
            }

            locatario.Id = id;
            return await _locatarioData.UpdateAsync(locatario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID do locatário deve ser maior que zero.");

            // Verificar se o locatário tem aluguéis ativos
            var hasActiveRentals = await HasActiveRentalsAsync(id);
            if (hasActiveRentals)
                throw new InvalidOperationException("Não é possível excluir um locatário que possui aluguéis ativos.");

            return await _locatarioData.DeleteAsync(id);
        }

        public async Task<IEnumerable<Locatario>> GetByCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio.");

            return await _locatarioData.GetByCpfAsync(cpf);
        }

        public async Task<IEnumerable<Locatario>> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio.");

            return await _locatarioData.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<Locatario>> GetActiveLocatariosAsync()
        {
            var allLocatarios = await _locatarioData.GetAllAsync();
            return allLocatarios.Where(l => l.Ativo);
        }

        public async Task<IEnumerable<Aluguel>> GetRentalsByLocatarioAsync(int locatarioId)
        {
            if (locatarioId <= 0)
                throw new ArgumentException("ID do locatário deve ser maior que zero.");

            return await _aluguelData.GetAlugueisPorLocatarioAsync(locatarioId);
        }

        public async Task<bool> HasActiveRentalsAsync(int locatarioId)
        {
            if (locatarioId <= 0)
                throw new ArgumentException("ID do locatário deve ser maior que zero.");

            var rentals = await _aluguelData.GetAlugueisPorLocatarioAsync(locatarioId);
            return rentals.Any(a => a.Status == AluguelStatus.Ativo);
        }

        private void ValidateLocatario(Locatario locatario)
        {
            if (locatario == null)
                throw new ArgumentNullException(nameof(locatario));

            if (string.IsNullOrWhiteSpace(locatario.Nome))
                throw new ArgumentException("Nome do locatário é obrigatório.");

            if (string.IsNullOrWhiteSpace(locatario.Cpf))
                throw new ArgumentException("CPF do locatário é obrigatório.");

            if (string.IsNullOrWhiteSpace(locatario.Email))
                throw new ArgumentException("Email do locatário é obrigatório.");

            if (string.IsNullOrWhiteSpace(locatario.Telefone))
                throw new ArgumentException("Telefone do locatário é obrigatório.");

            // Validação básica de CPF (11 dígitos)
            if (locatario.Cpf.Length != 11 || !locatario.Cpf.All(char.IsDigit))
                throw new ArgumentException("CPF deve conter 11 dígitos numéricos.");

            // Validação básica de email
            if (!locatario.Email.Contains("@") || !locatario.Email.Contains("."))
                throw new ArgumentException("Email deve ter um formato válido.");

            // Validação de telefone (mínimo 10 dígitos)
            var phoneDigits = new string(locatario.Telefone.Where(char.IsDigit).ToArray());
            if (phoneDigits.Length < 10)
                throw new ArgumentException("Telefone deve ter pelo menos 10 dígitos.");

            if (locatario.Nome.Length < 3)
                throw new ArgumentException("Nome deve ter pelo menos 3 caracteres.");

            if (locatario.Nome.Length > 100)
                throw new ArgumentException("Nome não pode ter mais de 100 caracteres.");
        }
    }
} 