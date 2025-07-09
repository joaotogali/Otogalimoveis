namespace Otogalimoveis.Domain.Model
{
    public class Aluguel
    {
        public int Id { get; set; }
        public Imovel Imovel { get; set; } = null!;
        public int ImovelId { get; set; }
        public Locatario Locatario { get; set; } = null!;
        public int LocatarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public AluguelStatus Status { get; set; }
    }

    public enum AluguelStatus
    {
        Ativo = 0,
        Finalizado = 1,
        Cancelado = 2
    }
}
