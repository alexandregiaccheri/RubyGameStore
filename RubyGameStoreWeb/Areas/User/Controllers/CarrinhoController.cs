using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarrinhoVM CarrinhoVM { get; set; }

        public CarrinhoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CarrinhoVM = new()
            {
                ListaCarrinho = _unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto"),
                PedidoCabecalho = new PedidoCabecalho()
            };

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                if (itemCarrinho.Produto.PrecoPromo == 0)
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoNormal;
                }
                else
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoPromo;
                }
            }

            return View(CarrinhoVM);
        }

        public IActionResult CriarPedido()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var usuario = _unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == claim.Value);

            CarrinhoVM = new CarrinhoVM()
            {
                ListaCarrinho = _unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto"),
                PedidoCabecalho = new PedidoCabecalho()
                {
                    UsuarioId = usuario.Id,
                    NomeDestinatario = $"{usuario.NomeUsuario} {usuario.SobrenomeUsuario}",
                    TelefoneContato = usuario.TelefoneContato,
                    LogradouroEntrega = usuario.LogradouroUsuario,
                    CidadeEntrega = usuario.CidadeUsuario,
                    EstadoEntrega = usuario.EstadoUsuario,
                    CEPEntrega = usuario.CEPUsuario
                }
            };

            CarrinhoVM.PedidoCabecalho.Usuario = _unitOfWork.UsuarioRepo.GetFirstOrDefault(u => u.Id == claim.Value);
            if (RedirecionarCadastro(CarrinhoVM))
            {
                TempData["StatusMessage"] = "É necessário que você complete seu cadastro para poder realizar compras!";
                return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            }

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                if (itemCarrinho.Produto.PrecoPromo == 0)
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoNormal;
                }
                else
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoPromo;
                }
            }
            return View(CarrinhoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarPedido(CarrinhoVM CarrinhoVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim.Value != CarrinhoVM.PedidoCabecalho.UsuarioId) return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            CarrinhoVM.ListaCarrinho = _unitOfWork.CarrinhoRepo.GetAll(c => c.UsuarioId == claim.Value, incluirPropriedades: "Produto");
            CarrinhoVM.PedidoCabecalho.DataHoraPedido = DateTime.Now;
            CarrinhoVM.PedidoCabecalho.StatusPedido = Pedido.Pendente;
            CarrinhoVM.PedidoCabecalho.StatusPagamento = Pagamento.Pendente;
            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                if (itemCarrinho.Produto.PrecoPromo == 0)
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoNormal;
                }
                else
                {
                    CarrinhoVM.PedidoCabecalho.TotalPedido += itemCarrinho.Quantidade * itemCarrinho.Produto.PrecoPromo;
                }
            }

            _unitOfWork.PedidoCabecalhoRepo.Add(CarrinhoVM.PedidoCabecalho);
            _unitOfWork.Save();

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                PedidoDetalhes pedidoDetalhes = new PedidoDetalhes()
                {
                    ProdutoId = itemCarrinho.ProdutoId,
                    PedidoId = CarrinhoVM.PedidoCabecalho.Id,
                    Preco = itemCarrinho.PrecoAtual,
                    Quantidade = itemCarrinho.Quantidade
                };
                _unitOfWork.PedidoDetalhesRepo.Add(pedidoDetalhes);
                _unitOfWork.Save();
            }

            if (User.IsInRole("Empresa"))
            {
                return RedirectToAction("Confirmado", "Carrinho", new { id = CarrinhoVM.PedidoCabecalho.Id });
            }

            //Stripe
            else
            {
                var domain = "https://localhost:7213/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"User/Carrinho/Confirmado?id={CarrinhoVM.PedidoCabecalho.Id}",
                    CancelUrl = domain + $"User/Carrinho",
                };

                foreach (var item in CarrinhoVM.ListaCarrinho)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "brl",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Produto.Titulo
                            },
                            UnitAmount = (long)item.PrecoAtual * 100
                        },
                        Quantity = item.Quantidade
                    };
                    options.LineItems.Add(sessionLineItem);
                };

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.PedidoCabecalhoRepo.AtualizarStatusStripe(CarrinhoVM.PedidoCabecalho.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);

                return new StatusCodeResult(303);
            }
        }

        public IActionResult Confirmado(int id)
        {
            var pedidoCabecalho = _unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id);
            var listaCarrinho = _unitOfWork.CarrinhoRepo.GetAll(p => p.UsuarioId == pedidoCabecalho.UsuarioId).ToList();

            var claimsIdentity = new ClaimsIdentity(User.Claims);
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (pedidoCabecalho.UsuarioId != claim.Value) return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            if (User.IsInRole("Empresa"))
            {
                _unitOfWork.PedidoCabecalhoRepo.AtualizarStatus(pedidoCabecalho.Id, Pedido.Aprovado, Pagamento.Empresarial);
                _unitOfWork.CarrinhoRepo.RemoveRange(listaCarrinho);
                _unitOfWork.Save();
            }
            else
            {
                var service = new SessionService();
                var session = service.Get(pedidoCabecalho.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.PedidoCabecalhoRepo.AtualizarStatus(pedidoCabecalho.Id, Pedido.Aprovado, Pagamento.Aprovado);
                    _unitOfWork.CarrinhoRepo.RemoveRange(listaCarrinho);
                    _unitOfWork.Save();
                }
            }

            return View(id);
        }

        public IActionResult Incrementar(int carrinhoId)
        {
            var carrinhoDB = _unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            _unitOfWork.CarrinhoRepo.AdicionarAoCarrinho(carrinhoDB, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Decrementar(int carrinhoId)
        {
            var carrinhoDB = _unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            if (carrinhoDB.Quantidade == 1) _unitOfWork.CarrinhoRepo.Remove(carrinhoDB);
            else _unitOfWork.CarrinhoRepo.RemoverDoCarrinho(carrinhoDB, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remover(int carrinhoId)
        {
            var carrinhoDB = _unitOfWork.CarrinhoRepo.GetFirstOrDefault(c => c.Id == carrinhoId);
            _unitOfWork.CarrinhoRepo.Remove(carrinhoDB);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public bool RedirecionarCadastro(CarrinhoVM vm)
        {
            if (vm.PedidoCabecalho.Usuario.TelefoneContato == null ||
                vm.PedidoCabecalho.Usuario.LogradouroUsuario == null ||
                vm.PedidoCabecalho.Usuario.CidadeUsuario == null ||
                vm.PedidoCabecalho.Usuario.EstadoUsuario == null ||
                vm.PedidoCabecalho.Usuario.CEPUsuario == null) return true;
            else return false;
        }
    }
}