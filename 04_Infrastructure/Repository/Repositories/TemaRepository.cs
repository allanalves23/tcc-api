using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class TemaRepository : BaseRepository<Tema>, IRepository<Tema>
    {
        public TemaRepository(DbSet<Tema> temas, string connectionString) : base(temas, connectionString) { }
    }
}