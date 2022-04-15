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
        [Display(Name = "Tipo do Desconto")]
        public string TipoDesconto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Número de Usos (-1 = Ilimitado)")]
        public int QuantidadeUsos { get; set; }

        [Required]
        [Display(Name = "Data de validade do cupom")]
        public DateTime ValidadeCupom { get; set; }

        [Display(Name = "Valor do desconto em reais")]
        public double ValorDescontoReais { get; set; }

        [Display(Name = "Valor do desconto em porcentagem")]
        public int ValorDescontoPorcento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor máximo do desconto")]
        public double ValorMaximoDesconto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor mínimo requerido")]
        public double ValorRequerido { get; set; }

    }
}
