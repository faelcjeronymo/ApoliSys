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
            var cotacoes = _context.Cotacaos.Where(c => c.IdVeiculo == IdVeiculo).ToList().OrderBy(c => c.Status);

            ViewBag.IdVeiculo = IdVeiculo;

            return View(cotacoes);
        }

        [HttpGet]
        [Route("Veiculo/{IdVeiculo:int}/Cotacao/Cadastrar")]
        public IActionResult Cadastrar(int IdVeiculo)
        {
            Veiculo? veiculo = _context.Veiculos.SingleOrDefault(v => v.Id == IdVeiculo);

            if (veiculo == null)
            {
                return NotFound();
            }

            ViewBag.IdVeiculo = veiculo?.Id;

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Cotacao cotacao) 
        {
            if (cotacao.Cadastrar() == false) 
            {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Ocorreu um problema ao cadastrar a cotação."
                });
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação Cadastrada!",
                urlRedirecionamento = "Veiculo/" + cotacao.IdVeiculo + "/Cotacao",
            });
        }

        [HttpGet]
        [Route("Veiculo/{IdVeiculo:int}/Cotacao/Modificar/{IdCotacao:int}")]
        public IActionResult Modificar(int IdVeiculo, int IdCotacao) 
        {
            var cotacao = _context.Cotacaos
            .Where(c => c.IdVeiculo == IdVeiculo)
            .SingleOrDefault(c => c.Id == IdCotacao);

            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        [HttpPut]
        [Route("Cotacao/Modificar")]
        public IActionResult Modificar(Cotacao cotacao)
        {
            try
            {
                int IdCotacao = cotacao.Id;

                var Cotacao = _context.Cotacaos.SingleOrDefault(c => c.Id == IdCotacao);

                if (Cotacao != null)
                {
                    if (Cotacao.Modificar(cotacao) == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo de errado ao modificar a Cotação.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
                
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao modificar a Cotação.",
                });

                throw;
            }
            
            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação modificada!",
                urlRedirecionamento = "Veiculo/" + cotacao.IdVeiculo + "/Cotacao",
            });
        }
        
        [HttpPut]
        [Route("Cotacao/Aprovar/{IdCotacao:int}")]
        public IActionResult Aprovar(int IdCotacao) 
        {
            Cotacao? cotacao = _context.Cotacaos.SingleOrDefault(c => c.Id == IdCotacao);
            
            try
            {
                if (cotacao != null)
                {
                    int IdVeiculo = cotacao.IdVeiculo;
                    
                    if (cotacao.Aprovar() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao aprovar a Cotação.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao aprovar a Cotação.",
                });

                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação aprovada! ❤️",
                urlRedirecionamento = "Veiculo/" + cotacao?.IdVeiculo + "/Cotacao",
            });
        }

        [HttpPut]
        [Route("Cotacao/Negar/{IdCotacao:int}")]
        public IActionResult Negar(int IdCotacao) 
        {
            Cotacao? cotacao = _context.Cotacaos.SingleOrDefault(c => c.Id == IdCotacao);
            
            try
            {
                if (cotacao != null)
                {
                    int IdVeiculo = cotacao.IdVeiculo;
                    
                    if (cotacao.Negar() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao negar a Cotação.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao negar a Cotação.",
                });

                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação negada! 😔",
                urlRedirecionamento = "Veiculo/" + cotacao?.IdVeiculo + "/Cotacao",
            });
        }

        [HttpDelete]
        [Route("Cotacao/Remover/{IdCotacao:int}")]
        public IActionResult Remover(int IdCotacao)
        {
            Cotacao? cotacao = _context.Cotacaos.SingleOrDefault(c => c.Id == IdCotacao);
            
            try
            {
                if (cotacao != null)
                {
                    if (cotacao.Remover() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao remover a Cotação.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
                
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao remover a Cotação.",
                });
                
                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Cotação Removida!",
                urlRedirecionamento = "Veiculo/" + cotacao?.IdVeiculo + "/Cotacao",
            });
            
        }
    }
}