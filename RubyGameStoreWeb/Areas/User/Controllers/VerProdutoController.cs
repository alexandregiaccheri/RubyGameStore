using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System.Security.Claims;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    public class VerProdutoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerProdutoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Detalhes(int produtoId)
        {
            var produtoDB = _unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == produtoId);
            if (produtoDB == null) return NotFound();
            Carrinho carrinho = new Carrinho()
            {
                ProdutoId = produtoId,
                Produto = produtoDB,
                Quantidade = 1
            };
            return View(carrinho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Detalhes(Carrinho carrinho)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            carrinho.UsuarioId = claim.Value;

            Carrinho carrinhoDB = _unitOfWork.CarrinhoRepo.GetFirstOrDefault(
                c => c.UsuarioId == claim.Value && c.ProdutoId == carrinho.ProdutoId);

            if (carrinhoDB == null)
            {
                _unitOfWork.CarrinhoRepo.Add(carrinho);
            }

            else
            {
                _unitOfWork.CarrinhoRepo.AdicionarAoCarrinho(carrinhoDB, carrinho.Quantidade);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", "Carrinho");
        }
    }
}