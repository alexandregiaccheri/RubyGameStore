using RubyGameStore.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace RubyGameStore.Models.ViewModels
{
    public class CarrinhoVM
    {
        public IEnumerable<Carrinho> ListaCarrinho { get; set; }
        public PedidoCabecalho PedidoCabecalho { get; set; }
        [Display(Name = "Código do Cupom")]
        public string? CodCupom { get; set; }
        public double SelecionaFrete { get; set; }
        public double TotalFinal { get; set; }
    }
}
