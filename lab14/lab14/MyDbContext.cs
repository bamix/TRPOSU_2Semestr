using System.Data.Entity;

namespace lab14
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnection")
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Sales> Saleses { get; set; }
    }
}
