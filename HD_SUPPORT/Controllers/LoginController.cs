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
                usuarioLogando.ContadorLogin++;

                _httpContextAccessor.HttpContext.Session.SetString("UsuarioNome", usuarioLogando.Nome);
                _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioID", usuarioLogando.Id);
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioEmail", usuarioLogando.Email);

                ViewBag.UsuarioNome = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
                if (usuarioLogando.ContadorLogin >= 5)
                {
                    if(usuarioLogando.Categoria == "RH")
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("CategoriaUsuario", usuarioLogando.Categoria);
                    }
                    var codigo = _autenticacaoFatores.GerarCodigoDeConfirmacao();
                    _autenticacaoFatores.EnviarCodigoPorEmail(admin.Email, codigo);
                    _httpContextAccessor.HttpContext.Session.SetString("CodigoDeConfirmacao", codigo);
                    usuarioLogando.ContadorLogin = 0;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "AutenticacaoFatores");
                }
                _context.SaveChanges();
                if(usuarioLogando.Categoria == "RH")
                {
                    return RedirectToAction("Index", "Rh");
                }

                return RedirectToAction("Index", "Home");
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
        public IActionResult TermosCondicoes()
        {
            return View();
        }
    }
}
