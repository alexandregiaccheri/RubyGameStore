using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.ViewModels;
using RubyGameStore.Helper.StaticNames;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdutoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

                ListaClassificacao = new[]
                {
                    new SelectListItem { Value = Classificacao.ESRB_E, Text = Classificacao.ESRB_E },
                    new SelectListItem { Value = Classificacao.ESRB_E10, Text = Classificacao.ESRB_E10 },
                    new SelectListItem { Value = Classificacao.ESRB_T, Text = Classificacao.ESRB_T },
                    new SelectListItem { Value = Classificacao.ESRB_M, Text = Classificacao.ESRB_M },
                    new SelectListItem { Value = Classificacao.ESRB_A, Text = Classificacao.ESRB_A },
                    new SelectListItem { Value = Classificacao.ESRB_P, Text = Classificacao.ESRB_P },
                },

                ListaGeneros = new[]
                {
                    new SelectListItem { Value = Generos.Acao, Text = Generos.Acao },
                    new SelectListItem { Value = Generos.Aventura, Text = Generos.Aventura },
                    new SelectListItem { Value = Generos.Casual, Text = Generos.Casual },
                    new SelectListItem { Value = Generos.Corrida, Text = Generos.Corrida },
                    new SelectListItem { Value = Generos.Esporte, Text = Generos.Esporte },
                    new SelectListItem { Value = Generos.Estrategia, Text = Generos.Estrategia },
                    new SelectListItem { Value = Generos.Luta, Text = Generos.Luta },
                    new SelectListItem { Value = Generos.Plataforma, Text = Generos.Plataforma },
                    new SelectListItem { Value = Generos.Ritmo, Text = Generos.Ritmo },
                    new SelectListItem { Value = Generos.RPG, Text = Generos.RPG }
                },

                ListaJogadores = new[]
                {
                    new SelectListItem { Value= Jogadores.UmJogador, Text = Jogadores.UmJogador },
                    new SelectListItem { Value= Jogadores.MultiplayerOnline, Text = Jogadores.MultiplayerOnline },
                    new SelectListItem { Value= Jogadores.MultiplayerLocal, Text = Jogadores.MultiplayerLocal }
                },

                ListaPlataformas = new[]
                {
                    new SelectListItem { Value = Plataforma.NSW, Text = Plataforma.NSW },
                    new SelectListItem { Value = Plataforma.PC, Text = Plataforma.PC },
                    new SelectListItem { Value = Plataforma.PS4, Text = Plataforma.PS4 },
                    new SelectListItem { Value = Plataforma.PS5, Text = Plataforma.PS5 },
                    new SelectListItem { Value = Plataforma.XONE, Text = Plataforma.XONE },
                    new SelectListItem { Value = Plataforma.XSXS, Text = Plataforma.XSXS }
                }
            };

            if (id == null || id == 0)
            {
                //Criar
                return View(produtoVM);
            }
            else
            {
                //Atualizar
                produtoVM.Produto = _unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id);
                return View(produtoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProdutoVM produtoVM, IFormFile? boxArt, IFormFile? screen1, IFormFile? screen2,
            IFormFile? screen3, IFormFile? screen4, IFormFile? screen5, IFormFile? screen6)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (boxArt != null)
                {
                    string formato = Path.GetExtension(boxArt.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.BoxArt != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.BoxArt.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = "boxart_" + Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            boxArt.CopyTo(fileStream);
                        }
                        produtoVM.Produto.BoxArt = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen1 != null)
                {
                    string formato = Path.GetExtension(screen1.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot1 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot1.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen1.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot1 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen2 != null)
                {
                    string formato = Path.GetExtension(screen2.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot2 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot2.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen2.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot2 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen3 != null)
                {
                    string formato = Path.GetExtension(screen3.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot3 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot3.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen3.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot3 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen4 != null)
                {
                    string formato = Path.GetExtension(screen4.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot4 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot4.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen4.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot4 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen5 != null)
                {
                    string formato = Path.GetExtension(screen5.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot5 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot5.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen5.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot5 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (screen6 != null)
                {
                    string formato = Path.GetExtension(screen6.FileName);
                    if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
                    {
                        if (produtoVM.Produto.Screenshot6 != null)
                        {
                            var imgExistente = Path.Combine(wwwRootPath, produtoVM.Produto.Screenshot6.TrimStart('\\'));
                            if (System.IO.File.Exists(imgExistente))
                            {
                                System.IO.File.Delete(imgExistente);
                            }
                        }
                        string nomeArquivo = Guid.NewGuid().ToString();
                        string upload = Path.Combine(wwwRootPath, @$"images\produtos\{produtoVM.Produto.Id}");
                        Directory.CreateDirectory(upload);
                        using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                        {
                            screen6.CopyTo(fileStream);
                        }
                        produtoVM.Produto.Screenshot6 = @$"\images\produtos\{produtoVM.Produto.Id}\" + nomeArquivo + formato;
                    }
                    else
                    {
                        TempData["error"] = "Arquivo enviado com formato invalido!";
                        return RedirectToAction("Upsert", new { id = produtoVM.Produto.Id });
                    }
                }

                if (produtoVM.Produto.Id == 0)
                {
                    _unitOfWork.ProdutoRepo.Add(produtoVM.Produto);
                }
                else
                {
                    _unitOfWork.ProdutoRepo.Update(produtoVM.Produto);
                }
                _unitOfWork.Save();
                TempData["sucesso"] = "Produto adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produtoVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var listaProdutos = _unitOfWork.ProdutoRepo.GetAll();
            return Json(new { data = listaProdutos });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var buscaProduto = _unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id);

            if (buscaProduto == null)
            {
                return Json(new { sucesso = false, mensagem = "Produto não encontrado!" });
            }

            if (buscaProduto.BoxArt != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.BoxArt.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot1 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot1.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot2 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot2.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot3 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot3.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot4 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot4.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot5 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot5.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            if (buscaProduto.Screenshot6 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot6.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente))
                {
                    System.IO.File.Delete(imgExistente);
                }
            }

            _unitOfWork.ProdutoRepo.Remove(buscaProduto);
            _unitOfWork.Save();
            return Json(new { sucesso = true, mensagem = "Produto apagado com sucesso!" });
        }
        #endregion
    }
}
