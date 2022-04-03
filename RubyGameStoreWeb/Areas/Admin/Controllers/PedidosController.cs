using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper;
using RubyGameStore.Models.ViewModels;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PedidosController : Controller
    {
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
            PedidoVM pedidoVM = new PedidoVM()
            {
                PedidoCabecalho = unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id),
                PedidoDetalhes = unitOfWork.PedidoDetalhesRepo.GetAll(p => p.PedidoId == id, incluirPropriedades: "Produto")
            };
            return View(pedidoVM);
        }

        public IActionResult DefinirEntrega(int id)
        {
            return View();
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
