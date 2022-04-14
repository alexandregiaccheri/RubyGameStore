using System.Linq.Expressions;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll(string? incluirPropriedades = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? incluirPropriedades = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? incluirPropriedades = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
