using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portal_de_empleos.Models;
using Portal_de_empleos.Controllers.Recursos;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Portal_de_empleos.Controllers.Recursos.Enumerados;
using System.Globalization;
using System.Collections.Generic;

namespace Portal_de_empleos.Controllers
{
    public class AccesoController : Controller
    {
        Oferta oferta;
        Ofertante ofertante;
        Cookie cookie;
        int idUsuario;

        public AccesoController(IHttpContextAccessor httpContextAccessor)
        {
            oferta = new Oferta();
            ofertante = new Ofertante();
            cookie = new Cookie(httpContextAccessor);
            idUsuario = cookie.Valor();
        }

        // actions de usuario

        [HttpGet]
        public IActionResult Sesion()
        {            
            if (cookie.Existe())
            {
                var infoBasica = ofertante.ObtenerEntidad(idUsuario);
                ViewData["Ofertas"] = oferta.OfertasUsuario(idUsuario);
                return View(infoBasica);
            }
            return RedirectToAction("Login", "Home");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarInfo(Ofertante ofertante)
        {
            ofertante.ID = idUsuario;
            (var mensaje, var tipo) = ofertante.Actualizar();
            TempData["Mensaje"] = tipo? Alert.Exito(mensaje) : Alert.Error(mensaje, "correo");
            return RedirectToAction("Sesion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarContra(string vContrasena, string nContrasena)
        {
            (var mensaje, var tipo) = ofertante.ActualizarContra(idUsuario, vContrasena, nContrasena);
            TempData["Mensaje"] = tipo ? Alert.Exito(mensaje, "contrasena") : Alert.Error(mensaje, "contrasena");
            return RedirectToAction("Sesion");
        }

        // actions de oferta

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarOferta(Oferta oferta)
        {
            (var mensaje, var tipo) = oferta.Eliminar();
            TempData["Mensaje"] = tipo ? Alert.Exito(mensaje, "eliminado") : Alert.Error(mensaje);
            return RedirectToAction("Sesion");
        }

        [HttpGet]
        public IActionResult FiltrarOfertas(string busqueda)
        {
            if (cookie.Existe())
            {
                busqueda = busqueda.Trim().ToUpper() ?? "#!";
                ViewBag.Busqueda = busqueda;

                var datosOferta = oferta.OfertasUsuario(idUsuario);        
                var listadoFiltro = datosOferta
                        .Where(o =>
                        o.TITULO.ToUpper().Contains(busqueda) ||
                        o.EMPRESA.ToUpper().Contains(busqueda) ||
                        o.PROVINCIA.NOMBRE.ToUpper().Contains(busqueda) ||
                        o.CATEGORIA.NOMBRE.ToUpper().Contains(busqueda) ||
                        o.TIEMPO.ToUpper().Contains(busqueda) ||
                        o.MODALIDAD.ToUpper().Contains(busqueda) ||
                        o.FECHAHORA.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) == busqueda);

                return View(listadoFiltro.ToList());
            }
            return StatusCode(403);
        }

        [HttpGet]
        public IActionResult OrdenarOfertas(int id)
        {
            if (cookie.Existe())
            {
                var listaOferta = oferta.OfertasUsuario(idUsuario);
                var listaOrdenada = new List<Oferta>();
                switch (id)
                {
                    case (int)Ordenar.TITULO:
                       listaOrdenada = listaOferta.OrderBy(o => o.TITULO).ToList();
                    break;

                    case (int)Ordenar.MODALIDAD:
                        listaOrdenada = listaOferta.OrderBy(o => o.MODALIDAD).ToList();
                        break;

                    case (int)Ordenar.SUELDO:
                       listaOrdenada = listaOferta.OrderByDescending(o => o.SUELDO).ToList();
                        break;

                    case (int)Ordenar.CATEGORIA:
                        listaOrdenada = listaOferta.OrderBy(o => o.CATEGORIA.NOMBRE).ToList();
                        break;

                    case (int)Ordenar.PROVINCIA:
                        listaOrdenada = listaOferta.OrderBy(o => o.PROVINCIA.NOMBRE).ToList();
                        break;

                    default:
                        return StatusCode(404);
                }
                return Json(listaOrdenada);
            }
            return StatusCode(403);
        }

        [HttpGet]
        public IActionResult BorrarCookie()
        {
            cookie.Eliminar();
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
