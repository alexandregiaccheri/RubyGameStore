using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IPedidoDetalhesRepository : IRepository<PedidoDetalhes>
    {
        void Update(PedidoDetalhes pedidoDetalhes);

    }
}
