using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Models
{
    public class CmpShopDBContext : DbContext
    {
        public CmpShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public CmpShopDBContext()
        {
        }

        public DbSet<Osystem> Osystems { get; set; }
        public DbSet<Computer> Computers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=computer;user=root;password=");
        }
    }
}
