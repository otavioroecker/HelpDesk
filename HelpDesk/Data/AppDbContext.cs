using Microsoft.EntityFrameworkCore;
using HelpDesk.Model;

namespace HelpDesk.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
