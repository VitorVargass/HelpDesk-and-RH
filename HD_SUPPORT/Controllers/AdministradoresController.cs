using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HD_SUPPORT.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministradoresController(Contexto contexto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> IndexAdministradores()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;

                return View(await _contexto.Administradores.ToListAsync());
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult NovoAdministrador()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoAdministrador(Administrador administrador)
        {
            var novoRegistro = await _contexto.Administradores.FirstOrDefaultAsync(u => u.Email == administrador.Email);
            if (novoRegistro == null)
            {
                await _contexto.Administradores.AddAsync(administrador);
                await _contexto.SaveChangesAsync();

                return RedirectToAction("Index", "Login");
            }
            TempData["MensagemErro"] = $"Email Já existente";
            return RedirectToAction("Index", "login");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarAdministrador(int administradorId)
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
                Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
                return View(administrador);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarAdministrador(Administrador administrador)
        {

            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                _contexto.Administradores.Update(administrador);
                await _contexto.SaveChangesAsync();

                return RedirectToAction(nameof(IndexAdministradores));
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> DetalharAdministrador(int administradorId)
        {

            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;

                Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
                return View(administrador);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirAdministrador(int administradorId)
        {

            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;

                Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
                _contexto.Administradores.Remove(administrador);
                await _contexto.SaveChangesAsync();

                return RedirectToAction(nameof(IndexAdministradores));
            }
            return RedirectToAction("Index", "Login");
        }
        
    }
}
