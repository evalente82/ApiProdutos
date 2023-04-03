using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiProdutos.Model
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ler a pasta de Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Produto>()
            .HasOne(x => x.Categoria)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => x.CategoriaId);


        }

        // convenção para toda strinh(VARCHAR) sempre ser criada com tamanho de 150
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        public DbSet<Categoria> Categoria{ get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
