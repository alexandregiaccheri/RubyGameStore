using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> listaProdutos = unitOfWork.ProdutoRepo.GetAll(incluirPropriedades:"Categoria,Plataforma");
            return View(listaProdutos);
        }

        public IActionResult Detalhes(int produtoId)
        {
            var query = unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == produtoId, incluirPropriedades: "Categoria,Plataforma");
            if (query == null) return NotFound();
            Carrinho carrinho = new Carrinho()
            {
                Produto = query,
                ProdutoId = produtoId,
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

            Carrinho carrinhoDB = unitOfWork.CarrinhoRepo.GetFirstOrDefault(
                c => c.UsuarioId == claim.Value && c.ProdutoId == carrinho.ProdutoId);

            if (carrinhoDB == null)
            {
                unitOfWork.CarrinhoRepo.Add(carrinho);
            }

            else
            {
                unitOfWork.CarrinhoRepo.Update(carrinho);
            }

            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}