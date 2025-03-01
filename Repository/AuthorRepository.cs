using Books2Gather.Models;
using Microsoft.EntityFrameworkCore;

namespace Books2Gather.Repository;

internal class AuthorRepository : IRepository<Author>
{
    private readonly AppDbContext context;
    private readonly DbSet<Author> dbSet;

    public AuthorRepository()
    {
        context = new AppDbContext();
        dbSet = context.Set<Author>();
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