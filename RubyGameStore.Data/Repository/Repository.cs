using Microsoft.EntityFrameworkCore;
using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace RubyGameStore.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RubyGameStoreDbContext dbContext;

        internal DbSet<T> dbSet;

        public Repository(RubyGameStoreDbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> listaObj = dbSet;
            return listaObj.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
