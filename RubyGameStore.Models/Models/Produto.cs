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
        [Column(TypeName = "Date")]
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }

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
        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Display(Name = "Classificação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Classificacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Jogadores { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, 100)]
        [Display(Name = "Metascore (0 = TBD)")]
        public int Metascore { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Preço Normal")]
        public double PrecoNormal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Preço Promocional (0 = Preço Normal/Sem Promoção)")]
        public double PrecoPromo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Cover Art (16:9)")]
        public string BoxArt { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Link YouTube (embed)")]
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
