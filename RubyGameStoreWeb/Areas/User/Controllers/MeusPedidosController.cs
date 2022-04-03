using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.ViewModels;
using System.Security.Claims;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class MeusPedidosController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public MeusPedidosController(IUnitOfWork _unitOfWork)
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

        #region API CALLS
        public IActionResult GetAll()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var listaPedidos = unitOfWork.PedidoCabecalhoRepo.GetAll(p => p.UsuarioId == claim.Value);
            foreach (var pedido in listaPedidos)
            {
                pedido.DataPedido = pedido.DataHoraPedido.ToShortDateString();
            }
            return Json(new { data = listaPedidos });
        }
        #endregion
    }
}
