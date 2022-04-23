using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Models.ViewModels
{
    public class BuscaVM
    {
        public string? BuscaTexto { get; set; }
        public IEnumerable<Produto> ListaProdutos { get; set; }
        public IEnumerable<SelectListItem> ListaOrdenacao { get; set; }
        public string Ordenacao { get; set; }

        // Plataformas
        public bool NSW { get; set; }
        public bool PC { get; set; }
        public bool PS4 { get; set; }
        public bool PS5 { get; set; }
        public bool XOne { get; set; }
        public bool XSeries { get; set; }

        // Gêneros
        public bool Acao { get; set; }
        public bool Aventura { get; set; }
        public bool Casual { get; set; }
        public bool Esporte { get; set; }
        public bool Estrategia { get; set; }
        public bool Outros { get; set; }
        public bool RPG { get; set; }

    }
}
