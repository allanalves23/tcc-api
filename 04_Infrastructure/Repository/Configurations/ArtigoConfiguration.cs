using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class ArtigoConfiguration : IEntityTypeConfiguration<Artigo>
    {
        public void Configure(EntityTypeBuilder<Artigo> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Titulo).HasMaxLength(100).IsRequired();
            builder.Property(item => item.Descricao).HasMaxLength(255);
            builder.Property(item => item.Conteudo).HasColumnType("longtext");

            builder
                .HasOne(item => item.Autor)
                .WithMany(item => item.Artigos)
                .HasForeignKey(item => item.AutorId);

            builder.HasOne(item => item.Tema)
                .WithMany(item => item.Artigos)
                .HasForeignKey(item => item.TemaId);

            builder.HasOne(item => item.Categoria)
                .WithMany(item => item.Artigos)
                .HasForeignKey(item => item.CategoriaId);

            builder.ToTable("Artigos");
        }
    }
}