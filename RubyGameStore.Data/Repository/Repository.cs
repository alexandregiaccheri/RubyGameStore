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

        public IEnumerable<T> GetAll(string? incluirPropriedades = null)
        {
            IQueryable<T> query = dbSet;

            if (incluirPropriedades != null)
            {
                foreach (var propriedade in incluirPropriedades.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propriedade);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? incluirPropriedades = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);

            if (incluirPropriedades != null)
            {
                foreach (var propriedade in incluirPropriedades.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propriedade);
                }
            }
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
