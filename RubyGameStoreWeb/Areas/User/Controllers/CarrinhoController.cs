using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;
using System.Security.Claims;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CarrinhoVM CarrinhoVM { get; set; }

        public CarrinhoController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            CarrinhoVM = new CarrinhoVM()
            {
                ListaCarrinho = unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto,Produto.Plataforma")
            };
            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                itemCarrinho.PrecoAtual = PrecoBaseadoNaQuantidade(itemCarrinho.Quantidade, itemCarrinho.Produto.Preco,
                    itemCarrinho.Produto.Preco50, itemCarrinho.Produto.Preco100);
                CarrinhoVM.TotalCarrinho += itemCarrinho.Quantidade * itemCarrinho.PrecoAtual;
            }
            return View(CarrinhoVM);
        }

        public IActionResult Incrementar(int carrinhoId)
        {
            var carrinhoDB = unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            unitOfWork.CarrinhoRepo.AdicionarAoCarrinho(carrinhoDB, 1);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Decrementar(int carrinhoId)
        {
            var carrinhoDB = unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            if (carrinhoDB.Quantidade == 1) unitOfWork.CarrinhoRepo.Remove(carrinhoDB);
            else unitOfWork.CarrinhoRepo.RemoverDoCarrinho(carrinhoDB, 1);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remover(int carrinhoId)
        {
            var carrinhoDB = unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            unitOfWork.CarrinhoRepo.Remove(carrinhoDB);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private double PrecoBaseadoNaQuantidade(int quantidade, double preco, double preco50, double preco100)
        {
            if (quantidade > 100) return preco100;
            else if (quantidade >= 50 && quantidade < 100) return preco50;
            else return preco;
        }
    }
}
