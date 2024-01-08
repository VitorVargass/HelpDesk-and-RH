using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HD_SUPPORT.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly Contexto _contexto;

        public AdministradoresController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> IndexAdministradores()
        {
            return View(await _contexto.Administradores.ToListAsync());
        }

        [HttpGet]
        public IActionResult NovoAdministrador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NovoAdministrador(Administrador administrador)
        {
            await _contexto.Administradores.AddAsync(administrador);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexAdministradores));
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarAdministrador(int administradorId)
        {
            Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
            return View(administrador);
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarAdministrador(Administrador administrador)
        {
            _contexto.Administradores.Update(administrador);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexAdministradores));
        }

        [HttpGet]
        public async Task<IActionResult> DetalharAdministrador(int administradorId)
        {
            Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
            return View(administrador);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirAdministrador(int administradorId)
        {
            Administrador administrador = await _contexto.Administradores.FindAsync(administradorId);
            _contexto.Administradores.Remove(administrador);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexAdministradores));
        }
        
    }
}
