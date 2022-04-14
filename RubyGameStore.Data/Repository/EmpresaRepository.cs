using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public EmpresaRepository(RubyGameStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Empresa obj)
        {
            var queryObj = _dbContext.Empresas.FirstOrDefault(o => o.Id == obj.Id);
            if (queryObj != null)
            {
                queryObj.CEPEmpresa = obj.CEPEmpresa;
                queryObj.CidadeEmpresa = obj.CidadeEmpresa;
                queryObj.CNPJEmpresa = obj.CNPJEmpresa;
                queryObj.EstadoEmpresa = obj.EstadoEmpresa;
                queryObj.LogradouroEmpresa = obj.LogradouroEmpresa;
                queryObj.NomeEmpresa = obj.NomeEmpresa;
                queryObj.TelefoneEmpresa = obj.TelefoneEmpresa;
            }
        }

    }
}
