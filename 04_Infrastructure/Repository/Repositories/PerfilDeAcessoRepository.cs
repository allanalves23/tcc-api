using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class PerfilDeAcessoRepository : BaseRepository<PerfilDeAcesso>, IRepository<PerfilDeAcesso>
    {
        public PerfilDeAcessoRepository(DbSet<PerfilDeAcesso> perfisDeAcesso, string connectionString) : base(perfisDeAcesso, connectionString) { }
    }
}