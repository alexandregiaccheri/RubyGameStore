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
        private readonly IUnitOfWork _unitOfWork;
        public MeusPedidosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            PedidoVM pedidoVM = new PedidoVM()
            {
                PedidoCabecalho = _unitOfWork.PedidoCabecalhoRepo.GetFirstOrDefault(p => p.Id == id),
                PedidoDetalhes = _unitOfWork.PedidoDetalhesRepo.GetAll(p => p.PedidoId == id, incluirPropriedades: "Produto")
            };
            return View(pedidoVM);
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var listaPedidos = _unitOfWork.PedidoCabecalhoRepo.GetAll(p => p.UsuarioId == claim.Value);
            foreach (var pedido in listaPedidos)
            {
                pedido.DataPedido = pedido.DataHoraPedido.ToString("dd/MM/yy");
            }
            return Json(new { data = listaPedidos });
        }
        #endregion
    }
}
