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

namespace ApoliSys.Controllers
{
    public class SeguradoController : Controller
    {
        private readonly ILogger<SeguradoController> _logger;

        public SeguradoController(ILogger<SeguradoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
                    successo = 0,
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

            return Json(new {
                successo = 1,
                mensagem = "Segurado Cadastrado!",
            });
        }

    }
}