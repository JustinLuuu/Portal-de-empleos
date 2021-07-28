using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_de_empleos.Controllers.Recursos
{
    public static class Alert
    {
        private static string foto;

        public static string Exito(string mensaje)
        {           
            return "<script>Swal.fire({" +
                "title: 'ENHORABUENA',"+
            "imageUrl: '../img/pinguinofeliz2.png'," +
             $"html: '<strong style=\"color:green;\">¡ {mensaje} !</strong>',"+
            "imageWidth: 140," +
            "imageHeight: 160,"+
            "imageAlt: 'pinguino-feliz',"+
            "confirmButtonText: `Genial !`,"+
            "showConfirmButton: true"+
            "});</script>";
        }

        public static string Exito(string mensaje, string asunto)
        {
            switch (asunto) {

                case "contrasena":
                    foto = "candado.png";
                    break;

                case "eliminado":
                    foto = "basura.png";
                    break;
                // agregar mas "cases" en dado caso
            }

            return "<script>Swal.fire({" +
                "title: 'ENHORABUENA'," +
            $"imageUrl: '../img/{foto}'," +
             $"html: '<strong style=\"color:green;\">¡ {mensaje} !</strong>'," +
            "imageWidth: 140," +
            "imageHeight: 160," +
            "imageAlt: 'mensaje-exito'," +
            "confirmButtonText: `Genial !`," +
            "showConfirmButton: true" +
            "});</script>";
        }


        public static string Error(string mensaje)
        {
            return "<script>Swal.fire({" +
                "title: 'ATENCION'," +
            $"imageUrl: '../img/pinguinotriste.png'," +
             $"html: '<strong style=\"color:red;\">¡ {mensaje} !</strong>'," +
            "imageWidth: 140," +
            "imageHeight: 160," +
            "imageAlt: 'mensaje-error'," +
           "showDenyButton: true," +
            "denyButtonText: 'Cerrar'," +
            "showConfirmButton: false" +
            "});</script>";
        }

        public static string Error(string mensaje, string asunto)
        {
            switch (asunto)
            {
                case "contrasena":
                    foto = "candado.png";
                    break;
                case "correo":
                    foto = "correoexiste.png";
                    break;
                    // agregar mas "cases" en dado caso
            }

            return "<script>Swal.fire({" +
            "title: 'ATENCION'," +
           $"imageUrl: '../img/{foto}'," +
            $"html: '<strong style=\"color:red;\">¡ {mensaje} !</strong>'," +
           "imageWidth: 140," +
           "imageHeight: 140," +
           "imageAlt: 'mensaje-error'," +
          "showDenyButton: true," +
           "denyButtonText: 'Cerrar'," +
           "showConfirmButton: false" +
           "});</script>";
        }
    }
}
