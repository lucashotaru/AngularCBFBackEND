using AngularCBFBackEND.models;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<UsuarioModel> usuario { get; set; }
    }
}