using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        DbSet<Artigo> Artigos { get; set; }
    }
}