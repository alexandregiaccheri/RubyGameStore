using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.ViewModels;
using System.Drawing;

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
                    new SelectListItem { Value = Classificacao.ESRB_RP, Text = Classificacao.ESRB_RP },
                },

                ListaGeneros = new[]
                {
                    new SelectListItem { Value = Generos.Acao, Text = Generos.Acao },
                    new SelectListItem { Value = Generos.Aventura, Text = Generos.Aventura },
                    new SelectListItem { Value = Generos.Casual, Text = Generos.Casual },
                    new SelectListItem { Value = Generos.Esporte, Text = Generos.Esporte },
                    new SelectListItem { Value = Generos.Estrategia, Text = Generos.Estrategia },
                    new SelectListItem { Value = Generos.Outros, Text = Generos.Outros },
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
                // Verifica se algum arquivo foi enviado com o post
                if (boxArt != null) produtoVM.Produto.BoxArt = UploadImagem(boxArt, produtoVM.Produto.BoxArt);
                if (screen1 != null) produtoVM.Produto.Screenshot1 = UploadImagem(screen1, produtoVM.Produto.Screenshot1);
                if (screen2 != null) produtoVM.Produto.Screenshot2 = UploadImagem(screen2, produtoVM.Produto.Screenshot2);
                if (screen3 != null) produtoVM.Produto.Screenshot3 = UploadImagem(screen3, produtoVM.Produto.Screenshot3);
                if (screen4 != null) produtoVM.Produto.Screenshot4 = UploadImagem(screen4, produtoVM.Produto.Screenshot4);
                if (screen5 != null) produtoVM.Produto.Screenshot5 = UploadImagem(screen5, produtoVM.Produto.Screenshot5);
                if (screen6 != null) produtoVM.Produto.Screenshot6 = UploadImagem(screen6, produtoVM.Produto.Screenshot6);

                // Se o ID é zero, cria um novo registro
                if (produtoVM.Produto.Id == 0)
                {
                    _unitOfWork.ProdutoRepo.Add(produtoVM.Produto);
                    TempData["sucesso"] = "Produto adicionado com sucesso!";
                }

                // Se o ID não for zero, atualiza o registro existente
                else
                {
                    _unitOfWork.ProdutoRepo.Update(produtoVM.Produto);
                    TempData["sucesso"] = "Produto atualizado com sucesso!";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(produtoVM);
        }

        public string UploadImagem(IFormFile arquivo, string linkNoBanco)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            // Verifica se a extensão do arquivo é uma imagem compatível
            string formato = Path.GetExtension(arquivo.FileName);
            if (formato == ".jpg" || formato == ".jpeg" || formato == ".png" || formato == ".svg")
            {
                // Verifica se já existe algum arquivo referenciado no banco de dados
                if (linkNoBanco != null)
                {
                    // Se existe a referência, o arquivo é apagado para otimizar o armazenamento
                    var imgExistente = Path.Combine(wwwRootPath, linkNoBanco.TrimStart('\\'));
                    if (System.IO.File.Exists(imgExistente))
                    {
                        System.IO.File.Delete(imgExistente);
                    }

                }

                // São definidos o nome do arquivo e a pasta onde será armazenado
                string nomeArquivo = Guid.NewGuid().ToString();
                string upload = Path.Combine(wwwRootPath, @$"images\produtos");

                Directory.CreateDirectory(upload);

                // Stream para salvar o arquivo
                using (var fileStream = new FileStream(Path.Combine(upload, nomeArquivo + formato), FileMode.Create))
                {
                    arquivo.CopyTo(fileStream);
                    // Cria um novo bitmap para gerar um novo arquivo sem informações de dados (metadata)
                    // para otimizar o armazenamento e não trafegar informações desnecessárias
                    Bitmap cleanImage = new Bitmap(fileStream);
                    cleanImage.Save(Path.Combine(upload, "ruby_" + nomeArquivo + formato));
                    cleanImage.Dispose();
                }

                // Apaga o arquivo salvo com metadata
                System.IO.File.Delete(Path.Combine(upload, nomeArquivo + formato));

                // Link do arquivo a ser salvo no banco de dados
                return @$"\images\produtos\" + "ruby_" + nomeArquivo + formato;
            }

            else
            {
                TempData["error"] = "Arquivo enviado com formato invalido!";
                RedirectToAction("Index");
                return "erro";
            }

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
            if (id == 0 || id == null) return NotFound();

            var buscaProduto = _unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id);

            if (buscaProduto == null) return Json(new { sucesso = false, mensagem = "Produto não encontrado!" });

            if (buscaProduto.BoxArt != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.BoxArt.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot1 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot1.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot2 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot2.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot3 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot3.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot4 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot4.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot5 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot5.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            if (buscaProduto.Screenshot6 != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var imgExistente = Path.Combine(wwwRootPath, buscaProduto.Screenshot6.TrimStart('\\'));
                if (System.IO.File.Exists(imgExistente)) System.IO.File.Delete(imgExistente);
            }

            _unitOfWork.ProdutoRepo.Remove(buscaProduto);
            _unitOfWork.Save();

            return Json(new { sucesso = true, mensagem = "Produto apagado com sucesso!" });

        }
        #endregion
    }

}
