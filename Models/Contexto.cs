using Microsoft.EntityFrameworkCore;

namespace Sistema_Restaurante.Models
{
    public class Contexto : DbContext
    {
        public Contexto (DbContextOptions<Contexto> options ) : base(options)
        {}
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Garcom> Garcom { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<Prato> Prato { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaHasProduto> VendaHasProduto { get; set; }

    }
}
