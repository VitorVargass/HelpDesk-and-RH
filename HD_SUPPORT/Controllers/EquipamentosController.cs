using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HD_SUPPORT.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly Contexto _contexto;

        public EquipamentosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> IndexEquipamentos()
        {
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
