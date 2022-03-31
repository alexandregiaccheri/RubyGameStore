using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IPedidoCabecalhoRepository : IRepository<PedidoCabecalho>
    {
        void Update(PedidoCabecalho pedidoCabecalho);
        void AtualizarStatus(int id, string statusPedido, string? statusPagamento = null);
    }
}
