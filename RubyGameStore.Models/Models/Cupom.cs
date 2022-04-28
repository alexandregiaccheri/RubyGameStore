using System.ComponentModel.DataAnnotations;

namespace RubyGameStore.Models.Models
{
    public class Cupom
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Código do Cupom")]
        public string CodCupom { get; set; }

        [Required]
        public DateTime DataHoraCriacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Descrição do cupom")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Status")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo do Desconto (R$/%)")]
        public string TipoDesconto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Usos do Cupom (para usos ilimitados, coloque -1)")]
        public int QuantidadeUsos { get; set; }

        [Required]
        [Display(Name = "Data de validade do cupom")]
        public DateTime ValidadeCupom { get; set; }

        [Display(Name = "Valor do desconto (R$)")]
        public double? ValorDescontoReais { get; set; }

        [Display(Name = "Valor do desconto (%)")]
        public int? ValorDescontoPorcento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor máximo descontado (R$)")]
        public double ValorMaximoDesconto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor mínimo para conceder desconto (R$)")]
        public double ValorRequerido { get; set; }

    }
}
