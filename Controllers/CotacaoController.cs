using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApoliSys.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApoliSys.Controllers
{
    public class CotacaoController : Controller
    {
        ApoliSysContext _context = new ApoliSysContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        [Route("Cotacao/Modificar/{id:int}")]
        public IActionResult Modificar(int id) {
            var cotacao = _context.Cotacaos.FirstOrDefault(c => c.Id == id);

            return View(cotacao);
        }
    }
}