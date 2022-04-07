using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class PedidoDetalhes
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        [ValidateNever]
        public PedidoCabecalho PedidoCabecalho { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        [ValidateNever]
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public double Preco { get; set; }
    }
}
