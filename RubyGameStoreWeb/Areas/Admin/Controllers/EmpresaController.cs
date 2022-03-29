using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class EmpresaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public EmpresaController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var empresa = new Empresa();
                return View(empresa);
            }
            else
            {
                var empresa = unitOfWork.EmpresaRepo.GetFirstOrDefault(e => e.Id == id);
                if(empresa == null) return NotFound();
                return View(empresa);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                if (empresa.Id == 0)
                {
                    unitOfWork.EmpresaRepo.Add(empresa);
                    TempData["sucesso"] = "Empresa adicionada com sucesso!";
                }
                else
                {
                    unitOfWork.EmpresaRepo.Update(empresa);
                    TempData["sucesso"] = "Empresa atualizada com sucesso!";
                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var listaUsuariosCNPJ = unitOfWork.EmpresaRepo.GetAll();
            return Json(new { data = listaUsuariosCNPJ });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            
            var empresa = unitOfWork.EmpresaRepo.GetFirstOrDefault(e => e.Id == id);

            if (empresa == null)
            {
                return Json(new { sucesso = false, mensagem = "Empresa não encontrada!"});
            }

            unitOfWork.EmpresaRepo.Remove(empresa);
            unitOfWork.Save();
            return Json(new { sucesso = true, mensagem = "Empresa apagada com sucesso!" });
        }
        #endregion
    }
}
