using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ArtigoRepository : BaseRepository<Artigo>, IRepository<Artigo>
    {
        public ArtigoRepository(DbSet<Artigo> artigos, string connectionString) : base(artigos, connectionString) { }
    }
}