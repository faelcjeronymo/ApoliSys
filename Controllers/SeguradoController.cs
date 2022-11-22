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
        public JsonResult Salvar(Pessoa pessoa, Segurado segurado)
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
        [Route("Segurado/Modificar/{idSegurado:int}")]
        public IActionResult Modificar(int idSegurado) {

            _context.Segurados.Include(s => s.IdPessoaNavigation);
            var segurado = _context.Segurados.Find(idSegurado);

            if (segurado == null) {
                return NotFound();
            }

            return View(segurado);
        }
    }
}