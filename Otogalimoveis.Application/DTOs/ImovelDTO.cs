namespace Otogalimoveis.Application.DTOs
{
    public class ImovelDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public decimal ValorLocacao { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class CreateImovelDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public decimal ValorLocacao { get; set; }
    }

    public class UpdateImovelDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public decimal ValorLocacao { get; set; }
    }

    public class ImovelDetailDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public decimal ValorLocacao { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<AluguelDTO> Alugueis { get; set; } = new List<AluguelDTO>();
    }
} 