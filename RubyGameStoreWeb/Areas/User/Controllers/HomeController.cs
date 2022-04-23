using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;
using RubyGameStore.Models.ViewModels;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> listaProdutos = _unitOfWork.ProdutoRepo.GetAll();
            return View(listaProdutos);
        }

        public IActionResult Busca(int idx)
        {
            BuscaVM novaBusca = new BuscaVM();
            novaBusca.ListaProdutos = _unitOfWork.ProdutoRepo.GetAll();
            novaBusca.ListaOrdenacao = new[]
            {
                new SelectListItem { Value = Ordenacao.AZ, Text = Ordenacao.AZ },
                new SelectListItem { Value=Ordenacao.ZA, Text=Ordenacao.ZA },
                new SelectListItem { Value=Ordenacao.MaisNovos, Text=Ordenacao.MaisNovos },
                new SelectListItem { Value=Ordenacao.MenorPreco, Text=Ordenacao.MenorPreco },
                new SelectListItem { Value=Ordenacao.MaiorPreco, Text=Ordenacao.MaiorPreco }
            };
            ViewBag.IDX = idx;
            idx *= 10;
            // idx--;

            // Se não houver nenhum filtro aplicado, retorna instantaneamente para poupar recursos
            if (HttpContext.Session.GetString(Sessao.FiltroPlataformas) == null &&
                HttpContext.Session.GetString(Sessao.FiltroGeneros) == null &&
                HttpContext.Session.GetString(Sessao.FiltroBusca) == null)
            {                
                switch (HttpContext.Session.GetString(Sessao.Ordenacao))
                {
                    case Ordenacao.ZA:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderByDescending(p => p.Titulo);
                        break;
                    case Ordenacao.MaisNovos:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderByDescending(p => p.DataLancamento);
                        break;
                    case Ordenacao.MenorPreco:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderBy(p => p.PrecoNormal);
                        break;
                    case Ordenacao.MaiorPreco:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderByDescending(p => p.PrecoNormal);
                        break;
                    default:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderBy(p => p.Titulo);
                        break;
                }

                // Seleciona os elementos do banco de dados e os exibe de 10 em 10
                var lista9 = novaBusca.ListaProdutos.ToList();
                ViewBag.ProdutosCount = novaBusca.ListaProdutos.Count();
                if (idx < lista9.Count())                
                    novaBusca.ListaProdutos = lista9.GetRange(idx - 10, idx);                
                else                
                    novaBusca.ListaProdutos = lista9.GetRange(idx - 10, lista9.Count() % 10);                

                return View(novaBusca);
            }

            else
            {
                // Prepara as variáveis para comparar com o banco de dados de acordo com os filtros
                var arrayPlataformas = HttpContext.Session.GetString(Sessao.FiltroPlataformas).Split(",");
                if (arrayPlataformas[0] == "1")
                {
                    novaBusca.NSW = true;
                    arrayPlataformas[0] = Plataforma.NSW;
                }
                else arrayPlataformas[0] = "Pular";

                if (arrayPlataformas[1] == "1")
                {
                    novaBusca.PC = true;
                    arrayPlataformas[1] = Plataforma.PC;
                }
                else arrayPlataformas[1] = "Pular";

                if (arrayPlataformas[2] == "1")
                {
                    novaBusca.PS4 = true;
                    arrayPlataformas[2] = Plataforma.PS4;
                }
                else arrayPlataformas[2] = "Pular";

                if (arrayPlataformas[3] == "1")
                {
                    novaBusca.PS5 = true;
                    arrayPlataformas[3] = Plataforma.PS5;
                }
                else arrayPlataformas[3] = "Pular";

                if (arrayPlataformas[4] == "1")
                {
                    novaBusca.XOne = true;
                    arrayPlataformas[4] = Plataforma.XONE;
                }
                else arrayPlataformas[4] = "Pular";

                if (arrayPlataformas[5] == "1")
                {
                    novaBusca.XSeries = true;
                    arrayPlataformas[5] = Plataforma.XSXS;
                }
                else arrayPlataformas[5] = "Pular";

                var pulosPlataformas = 0;
                for (int i = 0; i < 6; i++)
                    if (arrayPlataformas[i] != "Pular") pulosPlataformas++;

                if (pulosPlataformas == 0)
                    novaBusca.ListaProdutos = _unitOfWork.ProdutoRepo.GetAll();

                else novaBusca.ListaProdutos = _unitOfWork.ProdutoRepo.GetAll(p =>
                    p.Plataforma == arrayPlataformas[0] || p.Plataforma == arrayPlataformas[1] ||
                    p.Plataforma == arrayPlataformas[2] || p.Plataforma == arrayPlataformas[3] ||
                    p.Plataforma == arrayPlataformas[4] || p.Plataforma == arrayPlataformas[5]);

                // Prepara as variáveis para comparar com o banco de dados de acordo com os filtros
                var arrayGeneros = HttpContext.Session.GetString(Sessao.FiltroGeneros).Split(",");
                if (arrayGeneros[0] == "1")
                {
                    novaBusca.Acao = true;
                    arrayGeneros[0] = Generos.Acao;
                }
                else arrayGeneros[0] = "Pular";

                if (arrayGeneros[1] == "1")
                {
                    novaBusca.Aventura = true;
                    arrayGeneros[1] = Generos.Aventura;
                }
                else arrayGeneros[1] = "Pular";

                if (arrayGeneros[2] == "1")
                {
                    novaBusca.Casual = true;
                    arrayGeneros[2] = Generos.Casual;
                }
                else arrayGeneros[2] = "Pular";

                if (arrayGeneros[3] == "1")
                {
                    novaBusca.Esporte = true;
                    arrayGeneros[3] = Generos.Esporte;
                }
                else arrayGeneros[3] = "Pular";

                if (arrayGeneros[4] == "1")
                {
                    novaBusca.Estrategia = true;
                    arrayGeneros[4] = Generos.Esporte;
                }
                else arrayGeneros[4] = "Pular";

                if (arrayGeneros[5] == "1")
                {
                    novaBusca.Outros = true;
                    arrayGeneros[5] = Generos.Outros;
                }
                else arrayGeneros[5] = "Pular";

                if (arrayGeneros[6] == "1")
                {
                    novaBusca.RPG = true;
                    arrayGeneros[6] = Generos.RPG;
                }
                else arrayGeneros[6] = "Pular";

                var pulosGeneros = 0;
                for (int i = 0; i < 7; i++)
                    if (arrayGeneros[i] != "Pular") pulosGeneros++;

                // Se houve algum gênero filtrado, realiza a query
                if (pulosGeneros != 0)
                {
                    novaBusca.ListaProdutos = novaBusca.ListaProdutos.Where(p =>
                        p.Genero == arrayGeneros[0] || p.Genero == arrayGeneros[1] || p.Genero == arrayGeneros[2] ||
                        p.Genero == arrayGeneros[3] || p.Genero == arrayGeneros[4] || p.Genero == arrayGeneros[5] ||
                        p.Genero == arrayGeneros[6]);
                }

                // Se o usuário digitou algo na busca, realiza a query
                if (HttpContext.Session.GetString(Sessao.FiltroBusca) != null)
                {
                    var texto = HttpContext.Session.GetString(Sessao.FiltroBusca).ToUpper().TrimStart().TrimEnd();
                    novaBusca.ListaProdutos = novaBusca.ListaProdutos.Where(p =>
                    p.Titulo.ToUpper().Contains(texto) || p.Desenvolvedor.ToUpper().Contains(texto) || p.Distribuidor.ToUpper().Contains(texto));
                }

                switch (HttpContext.Session.GetString(Sessao.Ordenacao))
                {
                    case Ordenacao.ZA:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderByDescending(p => p.Titulo);
                        break;
                    case Ordenacao.MaisNovos:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderBy(p => p.DataLancamento);
                        break;
                    case Ordenacao.MenorPreco:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderBy(p => p.PrecoNormal);
                        break;
                    case Ordenacao.MaiorPreco:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderByDescending(p => p.PrecoNormal);
                        break;
                    default:
                        novaBusca.ListaProdutos = novaBusca.ListaProdutos.OrderBy(p => p.Titulo);
                        break;
                }

                var lista9 = novaBusca.ListaProdutos.ToList();

                ViewBag.ProdutosCount = novaBusca.ListaProdutos.Count();
                if (idx < lista9.Count())                
                    novaBusca.ListaProdutos = lista9.GetRange(idx - 10, idx);                
                else                
                    novaBusca.ListaProdutos = lista9.GetRange(idx - 10, lista9.Count() % 10);                

                return View(novaBusca);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AplicarFiltros(BuscaVM buscaFiltrada)
        {
            ViewBag.IDX = 1;
            var selecaoPlataformas = "";
            if (buscaFiltrada.NSW == true) selecaoPlataformas += "1,";
            else selecaoPlataformas += "0,";
            if (buscaFiltrada.PC == true) selecaoPlataformas += "1,";
            else selecaoPlataformas += "0,";
            if (buscaFiltrada.PS4 == true) selecaoPlataformas += "1,";
            else selecaoPlataformas += "0,";
            if (buscaFiltrada.PS5 == true) selecaoPlataformas += "1,";
            else selecaoPlataformas += "0,";
            if (buscaFiltrada.XOne == true) selecaoPlataformas += "1,";
            else selecaoPlataformas += "0,";
            if (buscaFiltrada.XSeries == true) selecaoPlataformas += "1";
            else selecaoPlataformas += "0";

            var selecaoGeneros = "";
            if (buscaFiltrada.Acao == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.Aventura == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.Casual == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.Esporte == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.Estrategia == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.Outros == true) selecaoGeneros += "1,";
            else selecaoGeneros += "0,";
            if (buscaFiltrada.RPG == true) selecaoGeneros += "1";
            else selecaoGeneros += "0";

            if (buscaFiltrada.BuscaTexto != null)
                HttpContext.Session.SetString(Sessao.FiltroBusca, buscaFiltrada.BuscaTexto);
            HttpContext.Session.SetString(Sessao.FiltroPlataformas, selecaoPlataformas);
            HttpContext.Session.SetString(Sessao.FiltroGeneros, selecaoGeneros);

            switch (buscaFiltrada.Ordenacao)
            {                
                case Ordenacao.ZA:
                    HttpContext.Session.SetString(Sessao.Ordenacao, Ordenacao.ZA);
                    break;
                case Ordenacao.MaisNovos:
                    HttpContext.Session.SetString(Sessao.Ordenacao, Ordenacao.MaisNovos);
                    break;
                case Ordenacao.MenorPreco:
                    HttpContext.Session.SetString(Sessao.Ordenacao, Ordenacao.MenorPreco);
                    break;
                case Ordenacao.MaiorPreco:
                    HttpContext.Session.SetString(Sessao.Ordenacao, Ordenacao.MaiorPreco);
                    break;
                default:
                    HttpContext.Session.SetString(Sessao.Ordenacao, Ordenacao.AZ);
                    break;
            };

            return RedirectToAction("Busca", new { idx = 1});
        }

        public IActionResult LimparTexto()
        {
            HttpContext.Session.Remove(Sessao.FiltroBusca);
            return RedirectToAction("Busca");
        }

    }
}