using API.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Contexts
{
    public class ApiContext : IdentityDbContext<ApplicationUser>
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) => 
            base.OnModelCreating(builder);
    }
}