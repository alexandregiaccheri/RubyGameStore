using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Models.ViewModels
{
    public class ProdutoVM
    {
        public Produto Produto {get; set;}

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaPlataformas { get; set; }
    }
}
