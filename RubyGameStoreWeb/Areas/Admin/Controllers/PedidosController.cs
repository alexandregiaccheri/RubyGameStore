using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;
using Stripe;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            if (pedidoCabecalho.StatusPagamento == StaticDetails.PagamentoAprovado || pedidoCabecalho.StatusPagamento == StaticDetails.PagamentoConvenio)
            {
                pedidoCabecalho.StatusPedido = StaticDetails.StatusProcessamento;
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
            if (pedidoCabecalho.StatusPagamento == StaticDetails.PagamentoAprovado)
            {
                //Stripe
                var options = new RefundCreateOptions
                {
                    PaymentIntent = pedidoCabecalho.PaymentIntentId,
                    Reason = RefundReasons.RequestedByCustomer
                };
                var service = new RefundService();
                service.Create(options);
                pedidoCabecalho.StatusPedido = StaticDetails.StatusEstornado;
            }
            else
            {
                pedidoCabecalho.StatusPedido = StaticDetails.StatusCancelado;
            }
            unitOfWork.Save();
            return RedirectToAction("Detalhes", "Pedidos", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enviar(PedidoCabecalho pedidoCabecalho)
        {
            if (pedidoCabecalho.StatusPagamento == StaticDetails.PagamentoAprovado || pedidoCabecalho.StatusPagamento == StaticDetails.PagamentoConvenio)
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
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == StaticDetails.StatusAprovado);
                    break;
                case "processando":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == StaticDetails.StatusProcessamento);
                    break;
                case "concluido":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == StaticDetails.StatusEnviado);
                    break;
                case "cancelado":
                    listaPedidos = listaPedidos.Where(p => p.StatusPedido == StaticDetails.StatusCancelado);
                    break;
                default:
                    break;
            }
            return Json(new { data = listaPedidos });
        }
        #endregion
    }
}
