using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ePasieka.Models
{

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BeeGarden> beeGardens { get; set; }
        public DbSet<Beehive> beehives { get; set; }
        public DbSet<Nuc> nucs { get; set; }
        public DbSet<CalendarEvent> calendarEvents { get; set; }
        public DbSet<User> users { get; set; }
    }
}
