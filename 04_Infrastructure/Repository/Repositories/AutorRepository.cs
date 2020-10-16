using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AutorRepository : BaseRepository<Autor>, IRepository<Autor>
    {
        public AutorRepository(DbSet<Autor> autores, string connectionString) : base(autores, connectionString) { }
    }
}