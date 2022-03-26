using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IPlataformaRepository : IRepository<Plataforma>
    {
        void Update(Plataforma plataforma);
    }
}
