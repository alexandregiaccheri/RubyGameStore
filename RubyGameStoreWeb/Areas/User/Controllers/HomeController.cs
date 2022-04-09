using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System.Diagnostics;
using System.Security.Claims;

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
            IEnumerable<Produto> listaProdutos = _unitOfWork.ProdutoRepo.GetAll(incluirPropriedades: "Categoria,Plataforma");
            return View(listaProdutos);
        }
    }
}