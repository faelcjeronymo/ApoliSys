using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApoliSys.Controllers
{
    public class SeguradoController : Controller
    {
        private readonly ILogger<SeguradoController> _logger;
        private ApoliSysContext _context = new ApoliSysContext();

        public SeguradoController(ILogger<SeguradoController> logger)
        {
            _logger = logger;
        }

        // meudominio.com/Segurado/
        public IActionResult Index()
        {
            var Segurados = _context.Segurados.Include(s => s.IdPessoaNavigation);
            return View(Segurados.ToList().OrderBy(s => s.IdPessoaNavigation.Nome));
        }

        // /Segurado/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Pessoa pessoa, Segurado segurado)
        {

            if (pessoa.validarDataNascimento() == false) 
            {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite uma Data de Nascimento válida."
                });
            }

            if (pessoa.validarCpfCnpj() == false)
            {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite um CPF/CNPJ Válido.",
                });
            }

            if (pessoa.Cep != null) 
            {
                if (pessoa.validarCep() == false) {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Por favor, digite um CEP Válido.",
                        });
                }
            }

            if (segurado.Cadastrar(pessoa) == false) 
            {
                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao cadastrar o Segurado.",
                });
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Segurado Cadastrado!",
                urlRedirecionamento = "Segurado/",
            });
        }

        [HttpGet]
        [Route("Segurado/Modificar/{IdSegurado:int}")]
        public IActionResult Modificar(int IdSegurado) 
        {

            var segurado = _context.Segurados
            .Include(s => s.IdPessoaNavigation)
            .SingleOrDefault(s => s.Id == IdSegurado);

            if (segurado == null) 
            {
                return NotFound();
            }

            return View(segurado);
        }

        [HttpPut]
        [Route("Segurado/Modificar/{IdSegurado:int}")]
        public IActionResult Modificar(Segurado segurado)
        {

            int IdSegurado = segurado.Id;

            try
            {
                var Segurado = _context.Segurados.Include(s => s.IdPessoaNavigation).SingleOrDefault(s => s.Id == IdSegurado);

                if (Segurado != null) {

                    if (segurado.IdPessoaNavigation.validarDataNascimento() == false) 
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Por favor, digite uma Data de Nascimento válida."
                        });
                    }

                    if (segurado.IdPessoaNavigation.validarCpfCnpj() == false)
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Por favor, digite um CPF/CNPJ Válido.",
                        });
                    }

                    if (segurado.IdPessoaNavigation.Cep != null) 
                    {
                        if (segurado.IdPessoaNavigation.validarCep() == false) 
                        {
                                return Ok(new {
                                    sucesso = 0,
                                    mensagem = "Por favor, digite um CEP Válido.",
                                });
                        }
                    }

                    if (Segurado.Modificar(segurado) == false) 
                    {
                        return Ok(new {
                            sucesso = 0,
                            mensagem = "Algo deu errado ao modificar o Segurado.",
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                return Ok(new {
                    sucesso = 0,
                    mensagem = "Algo deu errado ao modificar o Segurado."
                });

                throw;
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Segurado Modificado!",
                urlRedirecionamento = "Segurado/"
            });
        }

        [HttpGet]
        [Route("Segurado/{id:int}")]
        public IActionResult Informacoes (int id) 
        {
            var segurado = _context.Segurados
            .Include(s => s.IdPessoaNavigation)
            .SingleOrDefault(s => s.Id == id);

            if (segurado == null) {
                return NotFound();
            }

            return View(segurado);
        }

        [HttpDelete]
        [Route("Segurado/Remover/{IdSegurado:int}")]
        public IActionResult Remover (int IdSegurado)
        {
            Segurado? segurado = _context.Segurados.Include(s => s.IdPessoaNavigation).SingleOrDefault(s => s.Id == IdSegurado);

            if (segurado != null)
            {
                if (segurado.Remover() == false)
                {
                    Ok(new {
                        sucesso = 0,
                        mensagem = "Algo deu errado ao remover o Segurado.",
                    });
                }
            }

            return Ok(new {
                sucesso = 1,
                mensagem = "Segurado Removido!",
                urlRedirecionamento = "Segurado/"
            });
        }
    }
}