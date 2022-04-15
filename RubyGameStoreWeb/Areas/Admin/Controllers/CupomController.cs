using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using RubyGameStore.Helper.StaticNames;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
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
