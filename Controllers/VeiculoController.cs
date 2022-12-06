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
            var veiculos = _context.Veiculos.Where(v => v.IdSegurado == IdSegurado).ToList().OrderBy(v => v.Marca);

            ViewBag.IdSegurado = IdSegurado;

            return View(veiculos);
        }

        [HttpGet]
        [Route("Segurado/{IdSegurado:int}/Veiculo/Cadastrar")]
        public IActionResult Cadastrar(int IdSegurado)
        {
            var segurado = _context.Segurados.SingleOrDefault(s => s.Id == IdSegurado);

            ViewBag.IdSegurado = segurado.Id;

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Veiculo veiculo) 
        {
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

        [HttpPut]
        [Route("Veiculo/Modificar")]
        public IActionResult Modificar(Veiculo veiculo) {
            try
            {
                int IdVeiculo = veiculo.Id;

                var Veiculo = _context.Veiculos.SingleOrDefault(v => v.Id == IdVeiculo);

                if (Veiculo != null)
                {
                    if (Veiculo.Modificar(veiculo) == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao modificar o Veículo.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao modificar o Veículo."
                });
                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Veículo Modificado!",
                urlRedirecionamento = "Segurado/" + veiculo.IdSegurado + "/Veiculo",
            });
        }

        [HttpGet]
        [Route("Segurado/{IdSegurado:int}/Veiculo/{IdVeiculo:int}")]
        public IActionResult Informacoes (int IdSegurado, int IdVeiculo) {
            var veiculo = _context.Veiculos
            .Where(v => v.IdSegurado == IdSegurado)
            .SingleOrDefault(v => v.Id == IdVeiculo);

            if (veiculo == null) {
                return NotFound();
            }

            return View(veiculo);
        }

        [HttpDelete]
        [Route("Veiculo/Remover/{IdVeiculo:int}")]
        public IActionResult Remover(int IdVeiculo)
        {
            var veiculo = _context.Veiculos
            .SingleOrDefault(v => v.Id == IdVeiculo);

            if (veiculo != null)
            {
                if (veiculo.Remover() == false)
                {
                    return Ok(new {
                        sucesso = 0,
                        mensagem = "Algo deu errado ao remover o Veículo."
                    });
                }
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Veículo removido!",
                urlRedirecionamento = "Segurado/" + veiculo.IdSegurado + "/Veiculo"
            });
        }
    }
}