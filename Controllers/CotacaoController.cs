using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApoliSys.Controllers
{
    public class CotacaoController : Controller
    {
        ApoliSysContext _context = new ApoliSysContext();
        
        [HttpGet]
        [Route("Veiculo/{IdVeiculo:int}/Cotacao")]
        public IActionResult Index(int IdVeiculo)
        {
            var cotacoes = _context.Cotacaos.Where(c => c.IdVeiculo == IdVeiculo).ToList();

            ViewBag.IdVeiculo = IdVeiculo;

            return View(cotacoes);
        }

        [HttpGet]
        [Route("Veiculo/{IdVeiculo:int}/Cotacao/Cadastrar")]
        public IActionResult Cadastrar(int IdVeiculo)
        {
            var veiculo = _context.Veiculos.FirstOrDefault(v => v.Id == IdVeiculo);

            ViewBag.IdVeiculo = veiculo.Id;

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Cotacao cotacao) 
        {
            if (cotacao.Cadastrar() == false) 
            {
                return UnprocessableEntity(new {
                    sucesso = 0,
                    mensagem = "Ocorreu um problema ao cadastrar a cotação."
                });
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação Cadastrada!"
            });
        }

        [HttpGet]
        [Route("Cotacao/Modificar/{id:int}")]
        public IActionResult Modificar(int id) {
            var cotacao = _context.Cotacaos.FirstOrDefault(c => c.Id == id);

            return View(cotacao);
        }
    }
}