using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class PedidoCabecalho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [ValidateNever]
        public Usuario Usuario { get; set; }


        //Pedido
        [Required]
        public DateTime DataHoraPedido { get; set; }

        public DateTime DataHoraEnvio { get; set; }

        [NotMapped]
        [ValidateNever]
        public string DataPedido { get; set; }

        [Required]
        public double TotalPedido { get; set; }

        public string? StatusPedido { get; set; }

        public string? StatusPagamento { get; set; }

        public string? CodRastreio { get; set; }

        public string? Transportadora { get; set; }

        public DateTime DataPagamento { get; set; }

        public DateTime DataVencimento { get; set; }


        //Stripe
        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }


        //Endereço
        [Required]
        public string TelefoneContato { get; set; }

        [Required]
        public string NomeDestinatario { get; set; }

        [Required]
        public string LogradouroEntrega { get; set; }

        [Required]
        public string CidadeEntrega { get; set; }

        [Required]
        public string EstadoEntrega { get; set; }

        [Required]
        public string CEPEntrega { get; set; }

    }
}
