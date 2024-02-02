using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HD_SUPPORT.Controllers
{
    public class LoginController : Controller
    {
        private readonly Contexto _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AutenticacaoFatores _autenticacaoFatores;

        public LoginController(Contexto context, IHttpContextAccessor httpContextAccessor, AutenticacaoFatores autenticacaoFatores)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _autenticacaoFatores = autenticacaoFatores;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Administrador admin)
        {
            var usuarioLogando = _context.Administradores.FirstOrDefault(u => u.Email == admin.Email && u.Senha == admin.Senha);


            if (usuarioLogando != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioNome", usuarioLogando.Nome);
                _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioID", usuarioLogando.Id);

                ViewBag.UsuarioNome = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");

                var codigo = _autenticacaoFatores.GerarCodigoDeConfirmacao();
                _autenticacaoFatores.EnviarCodigoPorEmail(admin.Email, codigo);

                _httpContextAccessor.HttpContext.Session.SetString("CodigoDeConfirmacao", codigo);

                return RedirectToAction("Index", "AutenticacaoFatores");
            }
            else
            {
                ModelState.AddModelError("", "Email ou senha inválidos.");
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
