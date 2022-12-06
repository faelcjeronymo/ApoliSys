using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApoliSys.Controllers
{
    public class ApoliceController : Controller
    {
        private ApoliSysContext _context = new ApoliSysContext();

        [HttpGet]
        [Route("Cotacao/{IdCotacao:int}/Apolice")]
        public IActionResult Index(int IdCotacao)
        {
            var apolice = _context.Apolices.Where(a => a.IdCotacao == IdCotacao).ToList();

            ViewBag.IdCotacao = IdCotacao;

            return View(apolice);
        }

        [HttpGet]
        [Route("Cotacao/{IdCotacao:int}/Apolice/Cadastrar")]
        public IActionResult Cadastrar(int IdCotacao)
        {
            //Todo - Refazer lógica de cadastro de apolices
            
            int IdUltimaApoliceEmitida = _context.Apolices.OrderByDescending(a => a.Id).First().Id;

            ViewBag.NumeroApolice = IdUltimaApoliceEmitida + 1;

            var cotacao = _context.Cotacaos.FirstOrDefault(c => c.Id == IdCotacao);

            ViewBag.IdCotacao = cotacao.Id;

            return View();
        }

        [HttpPost]
        [Route("Apolice/Cadastrar")]
        public IActionResult Cadastrar(Apolice apolice)
        {
            string mensagemValidacaoApolice = apolice.ValidarApolice();

            if (mensagemValidacaoApolice != "Ok") {
                return Ok(new {
                    sucesso = 0,
                    mensagem = mensagemValidacaoApolice
                });
            }

            if (apolice.Cadastrar() == false) {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao cadastrar a Apólice."
                });
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Apólice cadastrada!"
            });
        }

        [HttpGet]
        [Route("Apolice/Modificar/{id:int}")]
        public IActionResult Modificar(int id)
        {
            var apolice = _context.Apolices.FirstOrDefault(a => a.Id == id);

            return View(apolice);
        }
    }
}