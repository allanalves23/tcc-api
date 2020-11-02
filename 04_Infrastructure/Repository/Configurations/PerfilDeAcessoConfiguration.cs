using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class PerfilDeAcessoConfiguration : IEntityTypeConfiguration<PerfilDeAcesso>
    {
        public void Configure(EntityTypeBuilder<PerfilDeAcesso> builder)
        {
            builder.HasKey(item => item.UsuarioId);
            builder.Property(item => item.Perfil).IsRequired();

            builder.ToTable("PerfilDeAcesso");
        }
    }
}