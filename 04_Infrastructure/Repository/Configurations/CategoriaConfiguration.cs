using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Nome).HasMaxLength(255).IsRequired();
            builder.Property(item => item.Descricao).HasMaxLength(255);

            builder.Ignore(item => item.Artigos);
            builder.ToTable("Categorias");
        }
    }
}