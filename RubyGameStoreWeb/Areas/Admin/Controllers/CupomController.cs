using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Autorizacao.Admin},{Autorizacao.Funcionario}")]
    public class CupomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CupomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar()
        {
            Cupom cupom = new Cupom();
            ViewBag.ListaDesconto = new List<SelectListItem>
            {
                new SelectListItem { Text = TipoDesconto.Porcentagem, Value = TipoDesconto.Porcentagem },
                new SelectListItem { Text = TipoDesconto.Reais, Value = TipoDesconto.Reais }
            };
            return View(cupom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Cupom cupom)
        {

            cupom.DataHoraCriacao = DateTime.Now;
            cupom.Status = true;
            if (ModelState.IsValid)
            {
                if (cupom.TipoDesconto == TipoDesconto.Reais &&
                    cupom.ValorDescontoReais < 1)
                {
                    ViewData["erro"] = "Confira o tipo e valor do desconto!";
                    return View(cupom);
                }

                if (cupom.TipoDesconto == TipoDesconto.Porcentagem &&
                    cupom.ValorDescontoPorcento < 1)
                {
                    ViewData["erro"] = "Confira o tipo e valor do desconto!";
                    return View(cupom);
                }

                _unitOfWork.CupomRepo.Add(cupom);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(cupom);
        }

        public IActionResult Detalhes(int id)
        {
            var cupom = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.Id == id);
            if (cupom == null) return NotFound();
            return View(cupom);
        }

        public IActionResult Desativar(int cupomId)
        {
            var cupom = _unitOfWork.CupomRepo.GetFirstOrDefault(c => c.Id == cupomId);
            if (cupom == null) return NotFound();
            cupom.Status = false;
            _unitOfWork.Save();
            return RedirectToAction("Detalhes", new { id = cupomId });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var listaCupons = _unitOfWork.CupomRepo.GetAll();
            return Json(new { data = listaCupons });
        }
        #endregion
    }
}
