using Books2Gather.Models;
using Microsoft.EntityFrameworkCore;

namespace Books2Gather.Repository;

internal class AuthorRepository : IRepository<Author>
{
    private readonly AppDbContext context;
    private readonly DbSet<Author> dbSet;

    public AuthorRepository()
    {
        this.context = new AppDbContext();
        dbSet = this.context.Set<Author>(); // Ermöglicht den Zugriff auf den entsprechenden DbSet
    }

    public void Delete(Author entity)
    {
        dbSet.Remove(entity);
        context.SaveChanges();
    }

    public IEnumerable<Author> GetAll()
    {
        return dbSet.ToList();
    }

    public Author? GetById(int id)
    {
        return dbSet.Find(id);
    }

    public void Insert(Author entity)
    {
        dbSet.Add(entity);
        context.SaveChanges();
    }

    public void Update(Author entity)
    {
        dbSet.Update(entity);
        context.SaveChanges();
    }
}