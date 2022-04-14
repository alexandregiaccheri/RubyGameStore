using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]        
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }

        [NotMapped]
        [ValidateNever]
        public string AnoLancamento { get; set; }

        [NotMapped]
        [ValidateNever]
        public string DataFormatada { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Desenvolvedor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Distribuidor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Plataforma { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Classificacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Jogadores { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, 100)]
        [Display(Name = "MetaScore (Deixe zero caso ainda não possua metascore)")]
        public int Metascore { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10000)]
        [Display(Name = "Preço Normal")]
        public double PrecoNormal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, 10000)]
        [Display(Name = "Preço Promocional - Deixe ZERO para não aplicar promoção!")]
        public double PrecoPromo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Imagem - Banner principal")]
        public string BoxArt { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Link do trailer no YouTube")]
        public string LinkTrailer { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot1 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot2 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot3 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot4 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot5 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Captura de Tela")]
        public string Screenshot6 { get; set; }        
        
    }
}
