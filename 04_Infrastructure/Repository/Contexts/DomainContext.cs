using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;

namespace Repository
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions<DomainContext> options) : base(options) { }

        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArtigoConfiguration());
            builder.ApplyConfiguration(new AutorConfiguration());
            builder.ApplyConfiguration(new TemaConfiguration());
            builder.ApplyConfiguration(new CategoriaConfiguration());
        }
    }
}