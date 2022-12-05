using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [Route("Segurado/{IdSegurado:int}/Veiculo")]
        public IActionResult Index(int IdSegurado)
        {
            var veiculos = _context.Veiculos.Where(v => v.IdSegurado == IdSegurado).ToList();

            ViewBag.IdSegurado = IdSegurado;

            return View(veiculos);
        }

        [HttpGet]
        [Route("Segurado/{IdSegurado:int}/Veiculo/Cadastrar")]
        public IActionResult Cadastrar(int IdSegurado)
        {
            var segurado = _context.Segurados.FirstOrDefault(s => s.Id == IdSegurado);

            ViewBag.IdSegurado = segurado.Id;

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Veiculo veiculo) 
        {
            Debug.WriteLine(veiculo.Km.ToString());

            if (veiculo.Cadastrar() == false) 
            {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao cadastrar o Veículo."
                });
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Veículo Cadastrado!",
                urlRedirecionamento = "Segurado/" + veiculo.IdSegurado + "/Veiculo",
            });
        }

        [HttpGet]
        [Route("Segurado/{IdSegurado:int}/Veiculo/Modificar/{IdVeiculo:int}")]
        public IActionResult Modificar(int IdSegurado, int IdVeiculo) {

            var veiculo = _context.Veiculos
            .Where(v => v.IdSegurado == IdSegurado)
            .SingleOrDefault(v => v.Id == IdVeiculo);

            if (veiculo == null) {
                return NotFound();
            }

            return View(veiculo);
        }

        /* [HttpPost]
        [Route("Veiculo/Modificar/{IdVeiculo:int}")]
        public IActionResult Modificar(int IdVeiculo) {
            
        } */

        [HttpGet]
        [Route("Segurado/{IdSegurado:int}/Veiculo/{idVeiculo:int}")]
        public IActionResult Informacoes (int IdSegurado, int idVeiculo) {
            var veiculo = _context.Veiculos
            .Where(v => v.IdSegurado == IdSegurado)
            .FirstOrDefault(v => v.Id == idVeiculo);

            if (veiculo == null) {
                return NotFound();
            }

            return View(veiculo);
        }
    }
}