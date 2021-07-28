using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Portal_de_empleos.Models.General
{
    public abstract class Entidades
    {
        protected SqlCommand COMANDO { get; set; }
        protected SqlDataReader LECTOR { get; set; }
        protected SqlConnection CONEXION { get; } = new SqlConnection(CadenaConexion.DevolverCadena());
        public List<IFormFile> FICHEROS { get; set; } = new List<IFormFile>();

        public abstract Entidades ObtenerEntidad(int id);

        protected byte[] ProcesarFichero()
        {
            byte[] bytes = null;
            foreach (IFormFile postedFile in FICHEROS)
            {               
                using (MemoryStream ms = new MemoryStream())
                {
                    postedFile.CopyTo(ms);
                    bytes = ms.ToArray();
                }
            }
            return bytes;
        }
    }

}