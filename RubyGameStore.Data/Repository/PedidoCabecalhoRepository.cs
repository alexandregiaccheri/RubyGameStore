using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class PedidoCabecalhoRepository : Repository<PedidoCabecalho>, IPedidoCabecalhoRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public PedidoCabecalhoRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void AtualizarStatus(int id, string statusPedido, string? statusPagamento = null)
        {
            var pedidoDB = dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            if (pedidoDB != null)
            {
                pedidoDB.StatusPedido = statusPedido;
                if (statusPagamento != null)
                {
                    pedidoDB.StatusPagamento = statusPagamento;
                }
            }
        }

        public void AtualizarStatusStripe(int id, string sessionId, string paymentIntentId)
        {
            var pedidoDB = dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            pedidoDB.SessionId = sessionId;
            pedidoDB.PaymentIntentId = paymentIntentId;
        }

        public void Update(PedidoCabecalho pedidoCabecalho)
        {
            dbContext.PedidosCabecalho.Update(pedidoCabecalho);
        }
    }
}
