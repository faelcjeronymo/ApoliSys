using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApoliSys.Controllers
{
    public class VeiculoController : Controller
    {
        private ApoliSysContext _context = new ApoliSysContext();

        [HttpGet]
        [Route("Segurado/{idSegurado:int}/Veiculo")]
        public IActionResult Index(int idSegurado)
        {
            var veiculo = _context.Veiculos.ToList();

            return View(veiculo);
        }

        public IActionResult Cadastrar()
        {
            Veiculo veiculo = new Veiculo();

            return View(veiculo);
        }

        [HttpGet]
        [Route("Segurado/{idSegurado:int}/Veiculo/Modificar/{idVeiculo:int}")]
        public IActionResult Modificar(int idSegurado, int idVeiculo) {

            var veiculo = _context.Veiculos
            .Where(v => v.IdSegurado == idSegurado)
            .FirstOrDefault(v => v.Id == idVeiculo);

            if (veiculo == null) {
                return NotFound();
            }

            return View(veiculo);
        }

        [HttpGet]
        [Route("Segurado/{idSegurado:int}/Veiculo/{idVeiculo:int}")]
        public IActionResult Informacoes (int idSegurado, int idVeiculo) {
            var veiculo = _context.Veiculos
            .Where(v => v.IdSegurado == idSegurado)
            .FirstOrDefault(v => v.Id == idVeiculo);

            if (veiculo == null) {
                return NotFound();
            }

            return View(veiculo);
        }
    }
}