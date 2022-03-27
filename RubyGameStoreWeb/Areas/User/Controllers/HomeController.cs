﻿using Microsoft.AspNetCore.Mvc;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System.Diagnostics;

namespace RubyGameStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> listaProdutos = unitOfWork.ProdutoRepo.GetAll(incluirPropriedades:"Categoria,Plataforma");
            return View(listaProdutos);
        }

        public IActionResult Detalhes(int id)
        {
            var query = unitOfWork.ProdutoRepo.GetFirstOrDefault(p => p.Id == id, incluirPropriedades: "Categoria,Plataforma");
            if (query == null) return NotFound();
            Carrinho carrinho = new Carrinho()
            {
                Produto = query,
                Quantidade = 1
            };
            return View(carrinho);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}