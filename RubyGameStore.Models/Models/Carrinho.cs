using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantidade { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        [ValidateNever]
        public Produto Produto { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [NotMapped]
        public double PrecoAtual { get; set; }

    }
}
