using RubyGameStore.Models.Models;

namespace RubyGameStore.Models.ViewModels
{
    public class PedidoVM
    {
        public PedidoCabecalho PedidoCabecalho { get; set; }
        public IEnumerable<PedidoDetalhes> PedidoDetalhes { get; set; }
    }
}
