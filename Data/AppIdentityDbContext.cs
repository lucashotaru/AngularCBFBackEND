using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.Data
{
    public class AppIdentityDbContext : IdentityDbContext
    {
        public AppIdentityDbContext(DbContextOptions options)
        {
        }
    }
}