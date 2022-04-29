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

        [BindProperty]
        public CarrinhoVM CarrinhoVM { get; set; }

        public CarrinhoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CarrinhoVM = new CarrinhoVM()
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

            CarrinhoVM.TotalFinal = CarrinhoVM.PedidoCabecalho.TotalPedido;

            var cupomDB = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.CodCupom == HttpContext.Session.GetString(Sessao.CupomAplicado));
            if (cupomDB != null)
            {
                // Se econtrar o cupom, faz validações
                if (cupomDB.Status == false)
                {
                    TempData["erroCupom"] = "Cupom Inválido!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValidadeCupom < CarrinhoVM.PedidoCabecalho.DataHoraPedido || cupomDB.QuantidadeUsos == 0)
                {
                    TempData["erroCupom"] = "Cupom Expirado!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValorRequerido > CarrinhoVM.PedidoCabecalho.TotalPedido)
                {
                    TempData["erroCupom"] = $"Este cupom só concede descontos em compras a partir de R$ {cupomDB.ValorRequerido.ToString("N2")}!";
                    return View(CarrinhoVM);
                }

                // Passando por todas as validações, salva o desconto apropriado
                if (cupomDB.TipoDesconto == TipoDesconto.Reais)
                {
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = (double)cupomDB.ValorDescontoReais;
                }

                if (cupomDB.TipoDesconto == TipoDesconto.Porcentagem)
                {
                    var desconto = (CarrinhoVM.PedidoCabecalho.TotalPedido / 100) * (int)cupomDB.ValorDescontoPorcento;
                    if (desconto > cupomDB.ValorMaximoDesconto) desconto = cupomDB.ValorMaximoDesconto;
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = desconto;
                }

                CarrinhoVM.TotalFinal -= (double)CarrinhoVM.PedidoCabecalho.DescontoAplicado;
            }

            if (HttpContext.Session.GetString(Sessao.FreteAplicado) != null)
            {
                if (HttpContext.Session.GetString(Sessao.FreteAplicado) == "Normal")
                {
                    if (CarrinhoVM.PedidoCabecalho.TotalPedido >= 200)
                    {
                        CarrinhoVM.SelecionaFrete = 0;
                    }
                    else
                    {
                        CarrinhoVM.SelecionaFrete = 7.99;
                    }
                }
                else
                {
                    CarrinhoVM.SelecionaFrete = 17.99;
                }
                CarrinhoVM.TotalFinal += CarrinhoVM.SelecionaFrete;
            }
            return View(CarrinhoVM);
        }

        public IActionResult CriarPedido()
        {
            // Pega a identidade do usuário logado
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Busca o usuário no banco de dados
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

            CarrinhoVM.TotalFinal = CarrinhoVM.PedidoCabecalho.TotalPedido;

            var cupomDB = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.CodCupom == HttpContext.Session.GetString(Sessao.CupomAplicado));
            if (cupomDB != null)
            {
                // Se econtrar o cupom, faz validações
                if (cupomDB.Status == false)
                {
                    ViewData["erroCupom"] = "Cupom Inválido!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValidadeCupom < CarrinhoVM.PedidoCabecalho.DataHoraPedido)
                {
                    ViewData["erroCupom"] = "Cupom Expirado!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.QuantidadeUsos == 0)
                {
                    ViewData["erroCupom"] = "Este cupom já foi usado.";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValorRequerido > CarrinhoVM.PedidoCabecalho.TotalPedido)
                {
                    ViewData["erroCupom"] = "Valor insuficiente para este cupom. Adicione mais produtos ou remova o cupom para continuar.";
                    return View(CarrinhoVM);
                }

                // Passando por todas as validações, salva o desconto apropriado
                if (cupomDB.TipoDesconto == TipoDesconto.Reais)
                {
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = (double)cupomDB.ValorDescontoReais;
                    CarrinhoVM.TotalFinal -= (double)CarrinhoVM.PedidoCabecalho.DescontoAplicado;
                }

                if (cupomDB.TipoDesconto == TipoDesconto.Porcentagem)
                {
                    var desconto = (CarrinhoVM.PedidoCabecalho.TotalPedido / 100) * (int)cupomDB.ValorDescontoPorcento;
                    if (desconto > cupomDB.ValorMaximoDesconto) desconto = cupomDB.ValorMaximoDesconto;
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = desconto;
                    CarrinhoVM.TotalFinal -= (double)CarrinhoVM.PedidoCabecalho.DescontoAplicado;
                }

            }

            if (HttpContext.Session.GetString(Sessao.FreteAplicado) == "Normal")
            {
                if (CarrinhoVM.PedidoCabecalho.TotalPedido >= 200)
                {
                    CarrinhoVM.PedidoCabecalho.FreteAplicado = 0;
                }
                else
                {
                    CarrinhoVM.PedidoCabecalho.FreteAplicado = 7.99;
                }

            }

            else
            {
                CarrinhoVM.PedidoCabecalho.FreteAplicado = 17.99;
            }

            CarrinhoVM.TotalFinal += CarrinhoVM.PedidoCabecalho.FreteAplicado;

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


            // Verifica se existe um cupom na sessão e busca o cupom no banco
            var cupomDB = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.CodCupom == HttpContext.Session.GetString(Sessao.CupomAplicado));
            if (cupomDB != null)
            {
                // Se econtrar o cupom, faz validações
                if (cupomDB.Status == false)
                {
                    ViewData["erroCupom"] = "Cupom Inválido!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValidadeCupom < CarrinhoVM.PedidoCabecalho.DataHoraPedido)
                {
                    ViewData["erroCupom"] = "Cupom Expirado!";
                    return View(CarrinhoVM);
                }

                if (cupomDB.QuantidadeUsos == 0)
                {
                    ViewData["erroCupom"] = "Este cupom já foi usado.";
                    return View(CarrinhoVM);
                }

                if (cupomDB.ValorRequerido > CarrinhoVM.PedidoCabecalho.TotalPedido)
                {
                    ViewData["erroCupom"] = "Valor insuficiente para este cupom. Adicione mais produtos ou remova o cupom para continuar.";
                    return View(CarrinhoVM);
                }

                // Passando por todas as validações, salva o desconto apropriado
                if (cupomDB.TipoDesconto == TipoDesconto.Reais)
                {
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = (double)cupomDB.ValorDescontoReais;
                    if (cupomDB.QuantidadeUsos != -1) cupomDB.QuantidadeUsos -= 1;
                }

                if (cupomDB.TipoDesconto == TipoDesconto.Porcentagem)
                {
                    var desconto = (CarrinhoVM.PedidoCabecalho.TotalPedido / 100) * (int)cupomDB.ValorDescontoPorcento;
                    if (desconto > cupomDB.ValorMaximoDesconto) desconto = cupomDB.ValorMaximoDesconto;
                    CarrinhoVM.PedidoCabecalho.DescontoAplicado = desconto;
                    if (cupomDB.QuantidadeUsos != -1) cupomDB.QuantidadeUsos -= 1;
                }

                if (HttpContext.Session.GetString(Sessao.FreteAplicado) == "Normal")
                {
                    if (CarrinhoVM.PedidoCabecalho.TotalPedido >= 200)
                    {
                        CarrinhoVM.PedidoCabecalho.FreteAplicado = 0;
                    }
                    else
                    {
                        CarrinhoVM.PedidoCabecalho.FreteAplicado = 7.99;
                    }

                }

                else
                {
                    CarrinhoVM.PedidoCabecalho.FreteAplicado = 17.99;
                }

                _unitOfWork.Save();

            }

            _unitOfWork.PedidoCabecalhoRepo.Add(CarrinhoVM.PedidoCabecalho);
            _unitOfWork.Save();

            foreach (var itemCarrinho in CarrinhoVM.ListaCarrinho)
            {
                PedidoDetalhes pedidoDetalhes = new PedidoDetalhes()
                {
                    ProdutoId = itemCarrinho.ProdutoId,
                    PedidoId = CarrinhoVM.PedidoCabecalho.Id,
                    Preco = itemCarrinho.Produto.PrecoPromo != 0 ? itemCarrinho.Produto.PrecoPromo : itemCarrinho.Produto.PrecoNormal,
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
                var domain = "https://www.rgs.xande.dev/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"User/Carrinho/Confirmado?id={CarrinhoVM.PedidoCabecalho.Id}",
                    CancelUrl = domain + $"User/Carrinho",
                };

                string nomeProdutos = "| ";
                foreach (var item in CarrinhoVM.ListaCarrinho)
                {
                    nomeProdutos += $"{item.Produto.Titulo} x {item.Quantidade} | ";
                }

                double? desconto;
                if (CarrinhoVM.PedidoCabecalho.DescontoAplicado != null)
                {
                    desconto = CarrinhoVM.PedidoCabecalho.DescontoAplicado;
                }
                else
                {
                    desconto = 0;
                }

                var sessionLineItemProdutos = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = nomeProdutos                                                            
                        },
                        UnitAmount = (long)((CarrinhoVM.PedidoCabecalho.TotalPedido - desconto) * 100)
                    },
                    Quantity = 1
                };
                options.LineItems.Add(sessionLineItemProdutos);

                var sessionLineItemFrete = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Frete"
                        },
                        UnitAmount = (long)(CarrinhoVM.PedidoCabecalho.FreteAplicado * 100)
                    },
                    Quantity = 1
                };
                options.LineItems.Add(sessionLineItemFrete);

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

        public IActionResult AplicarCupom(string CodCupom)
        {
            var cupomDB = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.CodCupom == CodCupom);

            if (cupomDB == null)
            {
                TempData["erroCupom"] = "Cupom Inválido!";
                return RedirectToAction("Index");
            }

            if (cupomDB.Status == false)
            {
                TempData["erroCupom"] = "Cupom Expirado!";
                return RedirectToAction("Index");
            }

            if (cupomDB.QuantidadeUsos != -1 && cupomDB.QuantidadeUsos == 0)
            {
                TempData["erroCupom"] = "Cupom Expirado!";
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString(Sessao.CupomAplicado, cupomDB.CodCupom);
            return RedirectToAction("Index");

        }

        public IActionResult LimparCupom()
        {
            HttpContext.Session.Remove(Sessao.CupomAplicado);
            return RedirectToAction("Index");
        }

        public IActionResult DefinirFrete(int idFrete)
        {
            if (idFrete == 1)
            {
                HttpContext.Session.SetString(Sessao.FreteAplicado, "Normal");
                return RedirectToAction("Index");
            }

            else
            {
                HttpContext.Session.SetString(Sessao.FreteAplicado, "Expressa");
                return RedirectToAction("Index");
            }
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