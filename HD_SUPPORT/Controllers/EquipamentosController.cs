using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HD_SUPPORT.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EquipamentosController(Contexto contexto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> IndexEquipamentos()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
            }

            return View(await _contexto.Equipamentos.ToListAsync());
        }
        [HttpGet]
        public IActionResult NovoEquipamento()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NovoEquipamento(Equipamento equipamento)
        {
            await _contexto.Equipamentos.AddAsync(equipamento);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexEquipamentos));
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarEquipamento(int equipamentoId)
        {
            Equipamento equipamento = await _contexto.Equipamentos.FindAsync(equipamentoId);
            return View(equipamento);
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarEquipamento(Equipamento equipamento)
        {
            _contexto.Equipamentos.Update(equipamento);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexEquipamentos));
        }

        [HttpGet]
        public async Task<IActionResult> DetalharEquipamento(int equipamentoId)
        {
            Equipamento equipamento = await _contexto.Equipamentos.FindAsync(equipamentoId);
            return View(equipamento);
        }


        [HttpPost]
        public async Task<IActionResult> ExcluirEquipamento(int equipamentoId)
        {
            Equipamento equipamento = await _contexto.Equipamentos.FindAsync(equipamentoId);
            _contexto.Equipamentos.Remove(equipamento); 
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(IndexEquipamentos));
        }
    }
}
