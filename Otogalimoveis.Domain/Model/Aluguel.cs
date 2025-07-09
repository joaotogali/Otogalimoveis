namespace Otogalimoveis.Domain.Model
{
    public class Aluguel
    {
        public int Id { get; set; }
        public Imovel Imovel { get; set; }
        public int ImovelId { get; set; }
        public Locatario Locatario { get; set; }
        public int LocatarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
