using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Models.Models
{
    public class Plataforma
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Plataforma")]
        public string NomePlataforma { get; set; }
    }
}
