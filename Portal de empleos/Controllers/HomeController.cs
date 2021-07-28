using Microsoft.AspNetCore.Mvc;
using Portal_de_empleos.Models;
using Portal_de_empleos.Controllers.Recursos;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;

namespace Portal_de_empleos.Controllers
{
    public class HomeController : Controller
    {
        Cookie cookie;
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            cookie = new Cookie(httpContextAccessor);
        }

        public IActionResult Index()
        {
            return View();
        }

        // login metodos
        [HttpGet]
        public IActionResult Login()
        {
            if (cookie.Existe())
            {
                return RedirectToAction("Sesion", "Acceso");
            }

            return View();
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public IActionResult Login(Ofertante ofertante)
        {            
            var id = ofertante.Loguear();
            if (id != 0)
            {
                cookie.Setear(id);
                return RedirectToAction("Sesion", "Acceso");
            }

            ViewData["NoExiste"] = false;
            return View();
        }


        // Registro metodos
        [HttpGet]
        public IActionResult Registro()
        {
            if (cookie.Existe())
            {
                return RedirectToAction("Sesion", "Acceso");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Registro([FromBody] Ofertante ofertante)
        {
            (var men, var tip) = ofertante.Insertar();
            return Json(new { mensaje = men, tipo = tip });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

