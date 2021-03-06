
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.Tabelas.Models;
using AngularCBFBackEND.conteudo.Usuarios.Models;
using AngularCBFBackEND.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Time>()
                .HasOne(time => time.Temporada)
                .WithMany(temporada => temporada.Times)
                .HasForeignKey(time => time.TemporadaId);
        }

        public DbSet<Time> Times { get; set; }

        public DbSet<Temporada> Temporadas { get; set; }

        public DbSet<JogosModel> jogos { get; set; }

        public DbSet<UsuariosModel> Usuarios { get; set; }

        public DbSet<TabelaDataModel> dataTabela { get; set; }

    }
}