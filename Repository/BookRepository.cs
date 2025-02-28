using Books2Gather.Models;
using Microsoft.EntityFrameworkCore;

namespace Books2Gather.Repository
{
    internal class BookRepository : IRepository<Book>
    {
        private readonly AppDbContext context;
        private readonly DbSet<Book> dbSet;

        public BookRepository()
        {
            this.context = new AppDbContext();
            dbSet = this.context.Set<Book>();
        }

        public void Delete(Book entity)
        {
            var existingBook = dbSet.Find(entity.BookId);
            if (existingBook != null)
            {
                dbSet.Remove(existingBook);
                context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.BookId == id);
        }

        public void Insert(Book entity)
        {
            if (entity.Author != null)
            {
                var existingAuthor = context.Authors.SingleOrDefault(a => a.AuthorId == entity.Author.AuthorId);
                if (existingAuthor == null)
                {
                    context.Authors.Add(entity.Author);
                }
                else
                {
                    context.Authors.Attach(existingAuthor);
                    entity.Author = existingAuthor;
                }
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }



        public void Update(Book entity)
        {
            var existingBook = dbSet.Find(entity.BookId);
            if (existingBook != null)
            {
                context.Entry(existingBook).CurrentValues.SetValues(entity);
                context.SaveChanges();
            }
        }

    }
}
