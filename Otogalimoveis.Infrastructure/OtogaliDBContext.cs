using Microsoft.EntityFrameworkCore;
using Otogalimoveis.Domain.Model;

namespace Otogalimoveis.Infrastructure
{
    public class OtogaliDBContext : DbContext
    {
        public OtogaliDBContext(DbContextOptions<OtogaliDBContext> options) : base(options){ }

        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Locatario> Locatario { get; set; }
    }
}
