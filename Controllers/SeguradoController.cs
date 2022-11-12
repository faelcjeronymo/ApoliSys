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
        public IActionResult Cadastrar(Pessoa pessoa)
        {
            try
            {
                //Removendo mascaras de formatacao
                pessoa.Cep = pessoa.Cep.Replace("-", "");
                pessoa.CpfCnpj = pessoa.CpfCnpj.Replace(".", "").Replace("-", "");
                pessoa.Celular = pessoa.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                ApoliSysContext _context = new ApoliSysContext();

                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                Debug.WriteLine(e.InnerException);
                return View();
            }

            return View();
        }

    }
}