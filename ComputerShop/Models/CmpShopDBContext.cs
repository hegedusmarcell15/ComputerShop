using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Models
{
    public class CmpShopDBContext : DbContext
    {
        public CmpShopDBContext(DbContextOptions options) : base(options)
        {
        }

        protected CmpShopDBContext()
        {
        }

        public DbSet<Osystem> Osystems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=computer;user=root;password=");
        }
    }
}
