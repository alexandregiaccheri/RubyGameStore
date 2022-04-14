using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IPedidoCabecalhoRepository : IRepository<PedidoCabecalho>
    {
        void AtualizarStatus(int id, string statusPedido, string? statusPagamento = null);
        void AtualizarStatusStripe(int id, string sessionId, string paymentIntentId);
        void DefinirEntrega(int id, string transportadora, string rastreio);
        void Update(PedidoCabecalho pedidoCabecalho);

    }
}
