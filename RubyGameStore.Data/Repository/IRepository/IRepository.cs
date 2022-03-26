using System.Linq.Expressions;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? incluirPropriedades = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? incluirPropriedades = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
