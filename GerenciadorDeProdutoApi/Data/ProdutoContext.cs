using GerenciadorDeProdutoApi.Models;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorDeProdutoApi.Data {
    public class ProdutoContext : DbContext {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
           
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
