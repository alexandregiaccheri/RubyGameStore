using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class PedidoCabecalhoRepository : Repository<PedidoCabecalho>, IPedidoCabecalhoRepository
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public PedidoCabecalhoRepository(RubyGameStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AtualizarStatus(int id, string statusPedido, string? statusPagamento = null)
        {
            var pedidoDB = _dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
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
            var pedidoDB = _dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            pedidoDB.SessionId = sessionId;
            pedidoDB.PaymentIntentId = paymentIntentId;
            pedidoDB.DataPagamento = DateTime.Now;
        }      

        public void DefinirEntrega(int id, string transportadora, string rastreio)
        {
            var pedidoDB = _dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            pedidoDB.Transportadora = transportadora;
            pedidoDB.CodRastreio = rastreio;
            pedidoDB.DataHoraEnvio = DateTime.Now;
            pedidoDB.StatusPedido = Pedido.Enviado;
        }

        public void Update(PedidoCabecalho pedidoCabecalho)
        {
            _dbContext.PedidosCabecalho.Update(pedidoCabecalho);
        }
    }
}
