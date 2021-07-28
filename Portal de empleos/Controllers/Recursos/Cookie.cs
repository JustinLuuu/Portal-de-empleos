using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;



namespace Portal_de_empleos.Controllers.Recursos
{
    public class Cookie : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private string KEY { get; } = "USUARIOIDVERIFICACION";

        public Cookie(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Setear(int id)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(5);
            httpContextAccessor.HttpContext.Response.Cookies.Append(KEY, id.ToString(), cookie);
        }

        public bool Existe()
        {
            return httpContextAccessor.HttpContext.Request.Cookies[KEY] != null ? true : false;
        }

        public int Valor()
        {
            var id = httpContextAccessor.HttpContext.Request.Cookies[KEY];
            return Convert.ToInt32(id);
        }

        public void Eliminar()
        {
            if (Existe())
                httpContextAccessor.HttpContext.Response.Cookies.Delete(KEY);
        }
    }
}
