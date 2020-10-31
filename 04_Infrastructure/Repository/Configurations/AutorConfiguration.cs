using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Nome).HasMaxLength(255).IsRequired();
            builder.Property(item => item.Email).HasMaxLength(255).IsRequired();

            builder.Ignore(item => item.Artigos);
            builder.ToTable("Autores");
        }
    }
}