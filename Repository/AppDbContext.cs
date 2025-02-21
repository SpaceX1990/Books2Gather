using Books2Gather.Models;
using Microsoft.EntityFrameworkCore;


namespace Books2Gather.Repository
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Buecherei;Trusted_Connection=True;");
        }

        public DbSet<Author> Authors { get; set; }
    }
}