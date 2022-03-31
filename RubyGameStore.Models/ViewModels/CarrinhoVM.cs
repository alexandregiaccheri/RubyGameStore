using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Models.ViewModels
{
    public class CarrinhoVM
    {
        [ValidateNever]
        public IEnumerable<Carrinho> ListaCarrinho { get; set; }
        public double TotalCarrinho { get; set; }
    }
}
