using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApoliSys.Controllers
{
    public class ApoliceController : Controller
    {
        private ApoliSysContext _context = new ApoliSysContext();

        [HttpGet]
        [Route("Cotacao/{IdCotacao:int}/Apolice")]
        public IActionResult Index(int IdCotacao)
        {
            var apolice = _context.Apolices.Where(a => a.IdCotacao == IdCotacao).ToList().OrderByDescending(a => a.NumeroApolice);

            ViewBag.IdCotacao = IdCotacao;

            return View(apolice);
        }

        [HttpGet]
        [Route("Cotacao/{IdCotacao:int}/Apolice/Cadastrar")]
        public IActionResult Cadastrar(int IdCotacao)
        {
            //Todo - Refazer lógica de cadastro de apolices
            int IdUltimaApoliceEmitida = 0;
            
            if (_context.Apolices.Count() != 0)
            {
                IdUltimaApoliceEmitida = _context.Apolices.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            }

            ViewBag.NumeroApolice = IdUltimaApoliceEmitida + 1;

            var cotacao = _context.Cotacaos.FirstOrDefault(c => c.Id == IdCotacao);

            ViewBag.IdCotacao = cotacao.Id;

            return View();
        }

        [HttpPost]
        [Route("Apolice/Cadastrar")]
        public IActionResult Cadastrar(Apolice apolice)
        {

            try
            {
                if (apolice.ValidarProcessoSusep() == false)
                {
                    return Ok(new {
                        sucesso = 0,
                        mensagem = "Por favor, forneça um N˙ de Processo Susep com até 20 caracteres.",
                    });
                }

                if (apolice.ValidarNumeroApolice() == false)
                {
                    return Ok(new {
                        sucesso = 0,
                        mensagem = "Por favor, forneça um N˙ de Apólice com até 9 caracteres.",
                    });
                }
                
                if (apolice.Cadastrar() == false) {
                    return Ok(new {
                        sucesso = 0,
                        mensagem = "Algo deu errado ao cadastrar a Apólice."
                    });
                }
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao cadastrar a Apólice.",
                });

                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Apólice cadastrada!",
                urlRedirecionamento = "Cotacao/" + apolice.IdCotacao + "/Apolice"
            });
        }

        [HttpGet]
        [Route("Cotacao/{IdCotacao:int}/Apolice/Modificar/{IdApolice:int}")]
        public IActionResult Modificar(int IdCotacao, int IdApolice)
        {
            var apolice = _context.Apolices
            .Where(a => a.IdCotacao == IdCotacao)
            .FirstOrDefault(a => a.Id == IdApolice);

            return View(apolice);
        }

        [HttpPut]
        [Route("Apolice/Modificar")]
        public IActionResult Modificar(Apolice apolice)
        {
            try
            {
                int IdApolice = apolice.Id;

                var Apolice = _context.Apolices.SingleOrDefault(a => a.Id == IdApolice);

                if (Apolice != null)
                {
                    if (Apolice.Modificar(apolice) == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao modificar a Apólice.",
                        });
                    }
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao modificar a Apólice.",
                });

                throw;
            }
            
            return Ok(new {
                sucesso = 1,
                mensagem = "Apólice Modificada!",
                urlRedirecionamento = "Cotacao/" + apolice.IdCotacao + "/Apolice",
            });
        }

        [HttpPut]
        [Route("Apolice/Cancelar/{IdApolice:int}")]
        public IActionResult Cancelar(int IdApolice)
        {
            Apolice? apolice = _context.Apolices.SingleOrDefault(a => a.Id == IdApolice);

            try
            {
                if (apolice != null)
                {
                    if (apolice.Cancelar() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao cancelar a Apólice",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao cancelar a Apólice",
                });

                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Apólice Cancelada!",
                urlRedirecionamento = "Cotacao/" + apolice.IdCotacao + "/Apolice",
            });
        }
        
        [HttpDelete]
        [Route("Apolice/Remover/{IdApolice:int}")]
        public IActionResult Remover(int IdApolice)
        {
            Apolice? apolice = _context.Apolices.SingleOrDefault(a => a.Id == IdApolice);

            try
            {
                if (apolice != null)
                {
                    if (apolice.Remover() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao remover a Apólice",
                        });
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao remover a Apólice",
                });

                throw;
            }


            return Ok(new {
                sucesso = 1,
                mensagem = "Apólice Removida!",
                urlRedirecionamento = "Cotacao/" + apolice.IdCotacao + "/Apolice",
            });
        }
        
    }
}