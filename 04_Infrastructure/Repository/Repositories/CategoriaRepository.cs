using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, IRepository<Categoria>
    {
        public CategoriaRepository(DbSet<Categoria> categorias, string connectionString) : base(categorias, connectionString) { }
    }
}