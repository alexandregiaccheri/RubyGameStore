using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        void Update(Empresa empresa);

    }
}
