using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otogalimoveis.Domain.Model
{
    public class Locatario
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
