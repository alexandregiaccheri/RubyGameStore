using System.ComponentModel.DataAnnotations;

namespace RubyGameStore.Models.Models
{
    public class Plataforma
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Plataforma")]
        public string NomePlataforma { get; set; }

        public override string ToString()
        {
            return this.NomePlataforma.ToString();
        }
    }
}
