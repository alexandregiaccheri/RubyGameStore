using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriaController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> listaObj = unitOfWork.CategoriaRepo.GetAll();
            return View(listaObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Categoria obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoriaRepo.Add(obj);
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

            var obj = unitOfWork.CategoriaRepo.GetFirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Categoria obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoriaRepo.Update(obj);
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

            var obj = unitOfWork.CategoriaRepo.GetFirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Categoria obj)
        {
            unitOfWork.CategoriaRepo.Remove(obj);
            unitOfWork.Save();
            TempData["sucesso"] = "Entrada excluída com sucesso";
            return RedirectToAction("Index");
        }
    }
}
