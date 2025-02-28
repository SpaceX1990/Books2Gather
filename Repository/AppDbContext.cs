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

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Der Herr der Ringe", ISBN = "918-3-673-12372-1", PublishingDate = new DateOnly(1954, 7, 29), Prize = 29.99m, AuthorId = 1, GenreId = 1 },
                new Book { BookId = 2, Title = "Harry Potter und der Stein der Weisen", ISBN = "978-3-551-55118-4", PublishingDate = new DateOnly(1997, 6, 26), Prize = 24.99m, AuthorId = 2, GenreId = 2 },
                new Book { BookId = 3, Title = "Der Hobbit", ISBN = "978-3-608-93000-1", PublishingDate = new DateOnly(1937, 9, 21), Prize = 19.99m, AuthorId = 1, GenreId = 1 }
            );
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "George R. R.", LastName = "Martin" },
                new Author { AuthorId = 2, FirstName = "William", LastName = "Shakespeare" }
            );
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Description = "Fantasy" },
                new Genre { GenreId = 2, Description = "Jugendbuch" }
            );
        }

    }
}