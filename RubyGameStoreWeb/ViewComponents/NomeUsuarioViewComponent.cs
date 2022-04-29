using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using System.Security.Claims;

namespace RubyGameStoreWeb.ViewComponents
{
    public class NomeUsuarioViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public NomeUsuarioViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var claimsidentity = (ClaimsIdentity)User.Identity;
                var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
                var usuario = _unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == claims.Value);

                HttpContext.Session.SetString(Sessao.NomeUsuario, usuario.NomeUsuario);
                return View("Default", HttpContext.Session.GetString(Sessao.NomeUsuario));
            }
            else
            {
                return View("Default");
            }
        }
    }
}
