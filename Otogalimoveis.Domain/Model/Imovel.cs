namespace Otogalimoveis.Domain.Model
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Endereco { get; set; }
        public decimal ValorLocacao { get; set; }
        public ImovelStatus Status { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; }
    }

    public enum ImovelStatus
    {
        Disponivel = 0,
        Alugado =1
    }
}
