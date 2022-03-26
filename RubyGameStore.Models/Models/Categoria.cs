using System.ComponentModel.DataAnnotations;

namespace RubyGameStore.Models.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public string NomeCategoria { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
