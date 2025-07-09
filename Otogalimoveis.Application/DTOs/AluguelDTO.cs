namespace Otogalimoveis.Application.DTOs
{
    public class AluguelDTO
    {
        public int Id { get; set; }
        public int ImovelId { get; set; }
        public string ImovelEndereco { get; set; } = string.Empty;
        public int LocatarioId { get; set; }
        public string LocatarioNome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? DuracaoEmDias { get; set; }
    }

    public class CreateAluguelDTO
    {
        public int ImovelId { get; set; }
        public int LocatarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }

    public class UpdateAluguelDTO
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class AluguelDetailDTO
    {
        public int Id { get; set; }
        public ImovelDTO Imovel { get; set; } = null!;
        public LocatarioDTO Locatario { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? DuracaoEmDias { get; set; }
    }
} 