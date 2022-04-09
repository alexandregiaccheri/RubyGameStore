using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;

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
            pedidoDB.DataPagamento = DateTime.Now;
        }

        //public PedidoCabecalho CriarNovoPedido(CarrinhoVM vm)
        //{
        //    return new PedidoCabecalho()
        //    {
        //        UsuarioId = vm.PedidoCabecalho.UsuarioId,
        //        DataHoraPedido = vm.PedidoCabecalho.DataHoraPedido,
        //        StatusPedido = 
        //    };
        //}

        public void DefinirEntrega(int id, string transportadora, string rastreio)
        {
            var pedidoDB = dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            pedidoDB.Transportadora = transportadora;
            pedidoDB.CodRastreio = rastreio;
            pedidoDB.DataHoraEnvio = DateTime.Now;
            pedidoDB.StatusPedido = Pedido.Enviado;
        }

        public void Update(PedidoCabecalho pedidoCabecalho)
        {
            dbContext.PedidosCabecalho.Update(pedidoCabecalho);
        }
    }
}
