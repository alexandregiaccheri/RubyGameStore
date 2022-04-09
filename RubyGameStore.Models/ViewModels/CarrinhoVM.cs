using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Models.ViewModels
{
    public class CarrinhoVM
    {
        public IEnumerable<Carrinho> ListaCarrinho { get; set; }
        public PedidoCabecalho PedidoCabecalho { get; set; }

    }
}
