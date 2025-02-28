//using Books2Gather.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Books2Gather.Repository;

//internal class GenreRepository : IRepository<Genre>
//{
//    private readonly AppDbContext context;
//    private readonly DbSet<Genre> dbSet;

//    public GenreRepository()
//    {
//        context = new AppDbContext();
//        dbSet = context.Set<Genre>();
//    }

//    public void Delete(Genre entity)
//    {
//        dbSet.Remove(entity);
//        context.SaveChanges();
//    }

//    public IEnumerable<Genre> GetAll()
//    {
//        return dbSet.ToList();
//    }

//    public Genre? GetById(int id)
//    {
//        return dbSet.Find(id);
//    }

//    public void Insert(Genre entity)
//    {
//        dbSet.Add(entity);
//        context.SaveChanges();
//    }

//    public void Update(Genre entity)
//    {
//        dbSet.Update(entity);
//        context.SaveChanges();
//    }
//}