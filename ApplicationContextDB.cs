using AngularCBFBackEND.models;
using Microsoft.EntityFrameworkCore;


namespace AngularCBFBackEND
{
    public class ApplicationContextDB : DbContext
    {
        public ApplicationContextDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SeriesModel> series{ get; set; }

    }
}