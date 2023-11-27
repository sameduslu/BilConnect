using Bilconnect_First_Version.Models;
using Microsoft.EntityFrameworkCore;

namespace Bilconnect_First_Version.data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }


        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
