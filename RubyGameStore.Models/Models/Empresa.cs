using System.ComponentModel.DataAnnotations;

namespace RubyGameStore.Models.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome Fantasia")]
        public string NomeEmpresa { get; set; }

        [Required]
        //[RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "Insira um CPF ou CNPJ")]
        [Display(Name = "CNPJ")]
        public string CNPJEmpresa { get; set; }

        [Required]
        [Display(Name = "Telefone móvel ou fixo (com DDD)")]
        //[RegularExpression(@"(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$", ErrorMessage = "Insira um número de telefone válido")]
        public string TelefoneEmpresa { get; set; }

        [Display(Name = "Logradouro (Rua, Nº) para entrega")]
        public string? LogradouroEmpresa { get; set; }

        [Display(Name = "Cidade")]
        public string? CidadeEmpresa { get; set; }

        [Display(Name = "Estado")]
        public string? EstadoEmpresa { get; set; }

        [Display(Name = "CEP para entrega")]
        public int? CEPEmpresa { get; set; }
    }
}
