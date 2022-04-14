using Microsoft.EntityFrameworkCore;
using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace RubyGameStore.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RubyGameStoreDbContext _dbContext;

        internal DbSet<T> _dbSet;

        public Repository(RubyGameStoreDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? incluirPropriedades = null)
        {
            IQueryable<T> query = _dbSet;

            if (incluirPropriedades != null)
            {
                foreach (var propriedade in incluirPropriedades.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propriedade);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? incluirPropriedades = null)
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(filter);

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
            IQueryable<T> query = _dbSet;

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
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
