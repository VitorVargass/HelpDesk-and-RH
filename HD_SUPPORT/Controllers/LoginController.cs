using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HD_SUPPORT.Controllers
{
    public class LoginController : Controller
    {
        private readonly Contexto _context;

        public LoginController(Contexto context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Administrador admin)
        {
            var usuarioLogando = _context.Administradores.FirstOrDefault(u => u.Email == admin.Email && u.Senha == admin.Senha);


            if (usuarioLogando != null)
            {
                Console.WriteLine("Esta funcionando");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");
            return View("Index");
        }
    }
}