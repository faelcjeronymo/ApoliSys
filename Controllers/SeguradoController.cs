using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApoliSys.Controllers
{
    public class SeguradoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}