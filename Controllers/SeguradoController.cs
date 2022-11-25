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

        public IActionResult Index()
        {
            var Segurados = _context.Segurados.Include(s => s.IdPessoaNavigation);
            return View(Segurados.ToList());
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SalvarCadastrar(Pessoa pessoa, Segurado segurado)
        {

            if (pessoa.validarDataNascimento() == false) {
                return Json(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite uma Data de Nascimento válida."
                });
            }

            if (pessoa.validarCpfCnpj() == false)
            {
                return Json(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite um CPF/CNPJ Válido.",
                });
            }

            if (pessoa.Cep != null) {
                if (pessoa.validarCep() == false) {
                        return Json(new {
                            sucesso = 0,
                            mensagem = "Por favor, digite um CEP Válido.",
                        });
                }
            }

            if (segurado.Cadastrar(pessoa) == false) {
                return Json(new {
                    sucesso = 0,
                    mensagem = "Erro ao cadastrar o segurado.",
                });
            }

            return Json(new {
                successo = 1,
                mensagem = "Segurado Cadastrado!",
            });
        }

        [HttpGet]
        [Route("Segurado/Modificar/{id:int}")]
        public IActionResult Modificar(int id) {

            var segurado = _context.Segurados
            .Include(s => s.IdPessoaNavigation)
            .FirstOrDefault(m => m.Id == id);

            if (segurado == null) {
                return NotFound();
            }

            return View(segurado);
        }

        [HttpPut]
        public JsonResult SalvarModificar(Pessoa pessoa, Segurado segurado)
        {

            var data = _context.Segurados.FirstOrDefault(s => s.Id == segurado.Id);

            if (pessoa.validarDataNascimento() == false) {
                return Json(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite uma Data de Nascimento válida."
                });
            }

            if (pessoa.validarCpfCnpj() == false)
            {
                return Json(new {
                    sucesso = 0,
                    mensagem = "Por favor, digite um CPF/CNPJ Válido.",
                });
            }

            if (pessoa.Cep != null) {
                if (pessoa.validarCep() == false) {
                        return Json(new {
                            sucesso = 0,
                            mensagem = "Por favor, digite um CEP Válido.",
                        });
                }
            }

            return Json(new {
                successo = 1,
                mensagem = "Segurado Modificado!",
            });
        }

        [HttpGet]
        [Route("Segurado/{id:int}")]
        public IActionResult Informacoes (int id) {
            var segurado = _context.Segurados
            .Include(s => s.IdPessoaNavigation)
            .FirstOrDefault(m => m.Id == id);

            if (segurado == null) {
                return NotFound();
            }

            return View(segurado);
        }
    }
}