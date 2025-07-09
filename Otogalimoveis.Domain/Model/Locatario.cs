namespace Otogalimoveis.Domain.Model
{
    public class Locatario
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;

        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}
