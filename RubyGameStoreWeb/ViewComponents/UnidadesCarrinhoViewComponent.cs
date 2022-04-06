using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper;
using System.Security.Claims;

namespace RubyGameStoreWeb.ViewComponents
{
    public class UnidadesCarrinhoViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnidadesCarrinhoViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var carrinho = _unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claims.Value);
            int totalCarrinho = 0;
            foreach (var c in carrinho)
            {
                totalCarrinho += c.Quantidade;
            }
            HttpContext.Session.SetInt32(StaticDetails.SessionUnidadesCarrinho, totalCarrinho);
            return View(HttpContext.Session.GetInt32(StaticDetails.SessionUnidadesCarrinho));
        }
    }
}
