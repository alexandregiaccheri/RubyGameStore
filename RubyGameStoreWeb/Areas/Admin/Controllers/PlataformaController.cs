using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlataformaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PlataformaController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Plataforma> listaObj = unitOfWork.PlataformaRepo.GetAll();
            return View(listaObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Plataforma obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.PlataformaRepo.Add(obj);
                unitOfWork.Save();
                TempData["sucesso"] = "Entrada criada com sucesso";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = unitOfWork.PlataformaRepo.GetFirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Plataforma obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.PlataformaRepo.Update(obj);
                unitOfWork.Save();
                TempData["sucesso"] = "Entrada atualizada com sucesso";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = unitOfWork.PlataformaRepo.GetFirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Plataforma obj)
        {
            unitOfWork.PlataformaRepo.Remove(obj);
            unitOfWork.Save();
            TempData["sucesso"] = "Entrada excluída com sucesso";
            return RedirectToAction("Index");
        }
    }
}
