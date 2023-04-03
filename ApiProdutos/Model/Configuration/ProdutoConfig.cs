using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProdutos.Model.Configuration
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Nome).HasMaxLength(100);
            builder.Property(x => x.Descricao).HasMaxLength(200);
            builder.Property(x => x.Preco).HasPrecision(10,2);
        }
    }
}
