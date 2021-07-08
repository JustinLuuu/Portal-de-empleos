using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_de_empleos.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Sesion()
        {
            return View();
        }
    }
}
