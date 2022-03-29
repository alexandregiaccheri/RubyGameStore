﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        [ValidateNever]
        public Produto Produto { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [ValidateNever]
        public Usuario Usuario { get; set; }
    }
}
