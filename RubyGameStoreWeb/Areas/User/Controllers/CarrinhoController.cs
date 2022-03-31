using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper;
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
                ListaCarrinho = unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto,Produto.Plataforma"),
                PedidoCabecalho = new PedidoCabecalho()
            };
            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                itemCarrinho.PrecoAtual = PrecoBaseadoNaQuantidade(itemCarrinho.Quantidade, itemCarrinho.Produto.Preco,
                    itemCarrinho.Produto.Preco50, itemCarrinho.Produto.Preco100);
                CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.PrecoAtual;
            }
            return View(CarrinhoVM);
        }

        public IActionResult RevisarPedido()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CarrinhoVM = new CarrinhoVM()
            {
                ListaCarrinho = unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto,Produto.Plataforma"),
                PedidoCabecalho = new PedidoCabecalho()
            };

            CarrinhoVM.PedidoCabecalho.Usuario = unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == claim.Value);

            CarrinhoVM.PedidoCabecalho.NomeDestinatario = $"{CarrinhoVM.PedidoCabecalho.Usuario.NomeUsuario} {CarrinhoVM.PedidoCabecalho.Usuario.SobrenomeUsuario}";
            CarrinhoVM.PedidoCabecalho.TelefoneContato = CarrinhoVM.PedidoCabecalho.Usuario.TelefoneContato;
            CarrinhoVM.PedidoCabecalho.LogradouroEntrega = CarrinhoVM.PedidoCabecalho.Usuario.LogradouroUsuario;
            CarrinhoVM.PedidoCabecalho.EstadoEntrega = CarrinhoVM.PedidoCabecalho.Usuario.EstadoUsuario;
            CarrinhoVM.PedidoCabecalho.CidadeEntrega = CarrinhoVM.PedidoCabecalho.Usuario.CidadeUsuario;
            CarrinhoVM.PedidoCabecalho.CEPEntrega = CarrinhoVM.PedidoCabecalho.Usuario.CEPUsuario;

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                itemCarrinho.PrecoAtual = PrecoBaseadoNaQuantidade(itemCarrinho.Quantidade, itemCarrinho.Produto.Preco,
                    itemCarrinho.Produto.Preco50, itemCarrinho.Produto.Preco100);
                CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.PrecoAtual;
            }

            return View(CarrinhoVM);
        }

        [HttpPost]
        [ActionName("RevisarPedido")]
        [ValidateAntiForgeryToken]
        public IActionResult RevisarPedidoPOST(CarrinhoVM CarrinhoVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CarrinhoVM.ListaCarrinho = unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto,Produto.Plataforma");

            CarrinhoVM.PedidoCabecalho.StatusPedido = StaticDetails.StatusPendente;
            CarrinhoVM.PedidoCabecalho.StatusPagamento = StaticDetails.PagamentoPendente;
            CarrinhoVM.PedidoCabecalho.DataHoraPedido = DateTime.Now;
            CarrinhoVM.PedidoCabecalho.UsuarioId = claim.Value;

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                itemCarrinho.PrecoAtual = PrecoBaseadoNaQuantidade(itemCarrinho.Quantidade, itemCarrinho.Produto.Preco,
                    itemCarrinho.Produto.Preco50, itemCarrinho.Produto.Preco100);
                CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.PrecoAtual;
            }

            unitOfWork.PedidoCabecalhoRepo.Add(CarrinhoVM.PedidoCabecalho);
            unitOfWork.Save();

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                PedidoDetalhes pedidoDetalhes = new PedidoDetalhes()
                {
                    ProdutoId = itemCarrinho.ProdutoId,
                    PedidoId = CarrinhoVM.PedidoCabecalho.Id,
                    Preco = itemCarrinho.PrecoAtual,
                    Quantidade = itemCarrinho.Quantidade
                };
                unitOfWork.PedidoDetalhesRepo.Add(pedidoDetalhes);
                unitOfWork.Save();
            }
            unitOfWork.CarrinhoRepo.RemoveRange(CarrinhoVM.ListaCarrinho);
            unitOfWork.Save();
            return RedirectToAction("Index", "Home");
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
