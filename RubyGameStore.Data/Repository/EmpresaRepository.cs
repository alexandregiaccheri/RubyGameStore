using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Data.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public EmpresaRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void Update(Empresa obj)
        {
            var queryObj = dbContext.Empresas.FirstOrDefault(o => o.Id == obj.Id);
            if (queryObj != null)
            {
                queryObj.NomeEmpresa = obj.NomeEmpresa;
                queryObj.CNPJEmpresa = obj.CNPJEmpresa;
                queryObj.TelefoneEmpresa = obj.TelefoneEmpresa;
                queryObj.LogradouroEmpresa = obj.LogradouroEmpresa;
                queryObj.CidadeEmpresa = obj.CidadeEmpresa;
                queryObj.EstadoEmpresa = obj.EstadoEmpresa;
                queryObj.CEPEmpresa = obj.CEPEmpresa;
            }
        }
    }
}
