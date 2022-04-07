using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubyGameStore.Models.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string SobrenomeUsuario { get; set; }
        public string TelefoneContato { get; set; }
        public string? LogradouroUsuario { get; set; }
        public string? CidadeUsuario { get; set; }
        public string? EstadoUsuario { get; set; }
        public string? CEPUsuario { get; set; }
        public int? EmpresaId { get; set; }
        [ForeignKey("EmpresaId")]
        [ValidateNever]
        public Empresa? Empresa { get; set; }
    }
}
