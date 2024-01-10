using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HD_SUPPORT.Controllers
{
    public class RhController : Controller
    {
        private readonly Contexto _contexto;

        public RhController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Equipamentos.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> DetalharEquipamento(int equipamentoId)
        {
            Equipamento equipamento = await _contexto.Equipamentos.FindAsync(equipamentoId);
            return View(equipamento);
        }
    }
}
