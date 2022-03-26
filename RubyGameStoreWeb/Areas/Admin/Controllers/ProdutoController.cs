using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.ViewModels;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutoController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IWebHostEnvironment webHostEnvironment;

        public ProdutoController(IUnitOfWork _unitOfWork, IWebHostEnvironment _webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProdutoVM produtoVM = new ProdutoVM()
            {
                Produto = new(),

                ListaCategorias = unitOfWork.CategoriaRepo.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.NomeCategoria,
                    Value = c.Id.ToString()
                }),

                ListaPlataformas = unitOfWork.PlataformaRepo.GetAll()
                .Select(p => new SelectListItem
                {
                    Text = p.NomePlataforma,
                    Value = p.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                //Criar
                return View(produtoVM);
            }
            else
            {
                //Atualizar
                produtoVM.Produto = unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id);
                return View(produtoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProdutoVM produtoVM, IFormFile? capa)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (capa != null)
                {
                    if (produtoVM.Produto.ImgCapaUrl != null)
                    {
                        var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.ImgCapaUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imgExistente))
                        {
                            System.IO.File.Delete(imgExistente);
                        }
                    }
                    string nomeArquivo = "capa_" + Guid.NewGuid().ToString();
                    string upload = Path.Combine(wwwRootPath, @"images\produtos");
                    string formato = Path.GetExtension(capa.FileName);
                    using (var fileStream = new FileStream(Path.Combine(
                        upload, nomeArquivo + formato), FileMode.Create))
                    {
                        capa.CopyTo(fileStream);
                    }
                    produtoVM.Produto.ImgCapaUrl = @"\images\produtos\" + nomeArquivo + formato;
                }
                if (produtoVM.Produto.Id == 0)
                {
                    unitOfWork.ProdutoRepo.Add(produtoVM.Produto);
                }
                else
                {
                    unitOfWork.ProdutoRepo.Update(produtoVM.Produto);
                }
                unitOfWork.Save();
                TempData["sucesso"] = "Produto adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produtoVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var listaProdutos = unitOfWork.ProdutoRepo.GetAll(incluirPropriedades: "Categoria,Plataforma");
            return Json(new { data = listaProdutos });
        }
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var buscaProduto = unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id);

            if (buscaProduto == null)
            {
                return Json(new { sucesso = false, mensagem = "Produto não encontrado!" });
            }

            if (buscaProduto.ImgCapaUrl != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.ImgCapaUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }
            unitOfWork.ProdutoRepo.Remove(buscaProduto);
            unitOfWork.Save();
            return Json(new { sucesso = true, mensagem = "Produto apagado com sucesso!" });
        }
        #endregion
    }
}
