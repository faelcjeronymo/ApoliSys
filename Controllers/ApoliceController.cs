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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
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