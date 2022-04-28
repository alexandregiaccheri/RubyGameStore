using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;
using Stripe;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Autorizacao.Admin},{Autorizacao.Funcionario}")]    
    public class PedidosController : Controller
    {
        [BindProperty]
        public PedidoVM PedidoVM { get; set; }

        private readonly IUnitOfWork unitOfWork;
        public PedidosController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            PedidoVM = new PedidoVM()
            {
                PedidoCabecalho = unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id),
                PedidoDetalhes = unitOfWork.PedidoDetalhesRepo.GetAll(p => p.PedidoId == id, incluirPropriedades: "Produto")
            };
            return View(PedidoVM);
        }

        public IActionResult Processar(int id)
        {
            var pedidoCabecalho = unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id);
            if (pedidoCabecalho.StatusPagamento == Pagamento.Aprovado || pedidoCabecalho.StatusPagamento == Pagamento.Empresarial)
            {
                pedidoCabecalho.StatusPedido = Pedido.Processando;
                unitOfWork.Save();
            }
            return RedirectToAction("Detalhes", "Pedidos", new { id = id });
        }

        public IActionResult Enviar(int id)
        {
            var pedidoCabecalho = unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id);
            return View(pedidoCabecalho);
        }

        public IActionResult Cancelar(int id)
        {
            var pedidoCabecalho = unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id);
            if (pedidoCabecalho.StatusPagamento == Pagamento.Aprovado)
            {
                //Stripe
                var options = new RefundCreateOptions
                {
                    PaymentIntent = pedidoCabecalho.PaymentIntentId,
                    Reason = RefundReasons.RequestedByCustomer
                };
                var service = new RefundService();
                service.Create(options);
                pedidoCabecalho.StatusPedido = Pedido.Estornado;
            }
            else
            {
                pedidoCabecalho.StatusPedido = Pedido.Cancelado;
            }
            unitOfWork.Save();
            return RedirectToAction("Detalhes", "Pedidos", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enviar(PedidoCabecalho pedidoCabecalho)
        {
            if (pedidoCabecalho.StatusPagamento == Pagamento.Aprovado || pedidoCabecalho.StatusPagamento == Pagamento.Empresarial)
            {
                unitOfWork.PedidoCabecalhoRepo.DefinirEntrega(pedidoCabecalho.Id, pedidoCabecalho.Transportadora, pedidoCabecalho.CodRastreio);
            }
            unitOfWork.Save();
            return RedirectToAction("Detalhes", "Pedidos", new { id = pedidoCabecalho.Id });
        }

        #region API CALLS
        public IActionResult GetAll(string status)
        {
            var listaPedidos = unitOfWork.PedidoCabecalhoRepo.GetAll(incluirPropriedades: "Usuario");
            foreach (var pedido in listaPedidos)
            {
                pedido.DataPedido = pedido.DataHoraPedido.ToShortDateString();
            }

            switch (status)
            {
                case "aprovado":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == Pedido.Aprovado);
                    break;
                case "processando":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == Pedido.Processando);
                    break;
                case "concluido":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == Pedido.Enviado);
                    break;
                case "cancelado":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == Pedido.Cancelado);
                    break;
                default:
                    break;
            }
            return Json(new { data = listaPedidos });
        }
        #endregion
    }
}
