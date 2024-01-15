using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HD_SUPPORT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
            }

                return View();
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
