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
    public class PedidoDetalhesRepository : Repository<PedidoDetalhes>, IPedidoDetalhesRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public PedidoDetalhesRepository(RubyGameStoreDbContext context) : base(context)
        {
            context = dbContext;
        }
        public void Update(PedidoDetalhes pedidoDetalhes)
        {
            dbContext.PedidosDetalhes.Update(pedidoDetalhes);
        }
    }
}
