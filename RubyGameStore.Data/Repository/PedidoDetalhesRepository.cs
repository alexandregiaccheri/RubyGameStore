using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class PedidoDetalhesRepository : Repository<PedidoDetalhes>, IPedidoDetalhesRepository
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public PedidoDetalhesRepository(RubyGameStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(PedidoDetalhes pedidoDetalhes)
        {
            _dbContext.PedidosDetalhes.Update(pedidoDetalhes);
        }
    }
}
