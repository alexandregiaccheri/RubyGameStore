using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [Range(1970, 2032)]
        [Display(Name = "Ano de lançamento")]
        public int Ano { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }     
        
        [Required]
        public string Desenvolvedor { get; set; }

        [Required]
        public string Distribuidor { get; set; }

        [Required]
        [ValidateNever]
        [Display(Name = "Imagem da capa")]
        public string ImgCapaUrl { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Preço listado")]
        public double PrecoListado { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Preço")]
        public double Preco { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Preço 50 - 100")]
        public double Preco50 { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Preço 100+")]
        public double Preco100 { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey ("CategoriaId")]
        [ValidateNever]
        public Categoria Categoria { get; set; }

        [Required]
        [Display(Name = "Plataforma")]
        public int PlataformaId { get; set; }

        [ForeignKey ("PlataformaId")]
        [ValidateNever]
        public Plataforma Plataforma { get; set; }
    }
}
