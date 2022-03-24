using Microsoft.AspNetCore.Mvc;

namespace RubyGameStoreWeb.Areas.Admin.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
