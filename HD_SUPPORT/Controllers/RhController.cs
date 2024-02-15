using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HD_SUPPORT.Controllers
{
    public class RhController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RhController(Contexto contexto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
                return View(await _contexto.Equipamentos.ToListAsync());
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> DetalharEquipamento(int equipamentoId)
        {
            
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
                Equipamento equipamento = await _contexto.Equipamentos.FindAsync(equipamentoId);
                return View(equipamento);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
