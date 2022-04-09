// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RubyGameStore.Data.Repository.IRepository;
using System.ComponentModel.DataAnnotations;

namespace RubyGameStoreWeb.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        
        [Display(Name = "Email")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Nome { get; set; }
            [Required]
            public string Sobrenome { get; set; }
            public string Telefone { get; set; }
            public string Logradouro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string CEP { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var usuario = _unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == user.Id);
            var telefone = usuario.TelefoneContato;
            var nome = usuario.NomeUsuario;
            var sobrenome = usuario.SobrenomeUsuario;
            var logradouro = usuario.LogradouroUsuario;
            var cidade = usuario.CidadeUsuario;
            var estado = usuario.EstadoUsuario;
            string cep = usuario.CEPUsuario;

            Username = userName;


            Input = new InputModel
            {
                Nome = nome,
                Sobrenome = sobrenome,
                Telefone = telefone,
                Logradouro = logradouro,
                Cidade = cidade,
                Estado = estado,
                CEP = cep,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar um usuário com o ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar um usuário com o ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var usuario = _unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == user.Id);
            usuario.NomeUsuario = Input.Nome;
            usuario.SobrenomeUsuario = Input.Sobrenome;
            usuario.TelefoneContato = Input.Telefone;
            usuario.LogradouroUsuario = Input.Logradouro;
            usuario.CidadeUsuario = Input.Cidade;
            usuario.EstadoUsuario = Input.Estado;
            usuario.CEPUsuario = Input.CEP;

            _unitOfWork.Save();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado com sucesso.";
            return RedirectToPage();
        }
    }
}
