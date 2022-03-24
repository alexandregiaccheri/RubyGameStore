using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Models.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeCategoria { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
