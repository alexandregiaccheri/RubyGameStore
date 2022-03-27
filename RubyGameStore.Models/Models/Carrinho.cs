using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Models.Models
{
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range (1, 1000)]
        public int Quantidade { get; set; }
        [Required]
        public Produto Produto { get; set; }
    }
}
