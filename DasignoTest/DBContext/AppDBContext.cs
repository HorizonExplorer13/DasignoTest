using DasignoTest.Entitys.Users;
using Microsoft.EntityFrameworkCore;

namespace DasignoTest.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> users { get; set; }
    }
}
