using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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
        [Range(1970, 2032)]
        [Display(Name = "Ano de lançamento")]
        public int Ano { get; set; }

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
        [Display(Name = "MetaScore (Deixe '0' caso 'tbd')")]
        public int Metascore { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10000)]
        [Display(Name = "Preço")]
        public double Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10000)]
        [Display(Name = "Preço 50 - 100")]
        public double Preco50 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10000)]
        [Display(Name = "Preço 100+")]
        public double Preco100 { get; set; }

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
        [Display(Name = "Screen1")]
        public string Screenshot1 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Screen2")]
        public string Screenshot2 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Screen3")]
        public string Screenshot3 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Screen4")]
        public string Screenshot4 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Screen5")]
        public string Screenshot5 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidateNever]
        [Display(Name = "Screen6")]
        public string Screenshot6 { get; set; }        
        
    }
}
