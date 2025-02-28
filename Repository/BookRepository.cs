using Books2Gather.Models;
using Microsoft.EntityFrameworkCore;

namespace Books2Gather.Repository {
    internal class BookRepository : IRepository<Book> {
        private readonly AppDbContext context;
        private readonly DbSet<Book> dbSet;

        public BookRepository() {
            this.context = new AppDbContext();
            dbSet = this.context.Set<Book>();
        }

        public void Delete(Book entity) {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            return dbSet
                .Include(b => b.Authors)  // Lädt die Autoren mit
                .Include(b => b.Genres)   // Lädt die Genres mit
                .ToList();
        }


        public Book? GetById(int id) {
            return dbSet.Find(id);
        }

        public void Insert(Book entity) {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(Book entity) {
            dbSet.Update(entity);
            context.SaveChanges();
        }
    }
}
