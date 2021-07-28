using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Portal_de_empleos.Models.General;
using Portal_de_empleos.Models.General.Interfaces;

namespace Portal_de_empleos.Models
{
    public class Ofertante : Entidades, Iinsert, IUpdate
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string EMAIL { get; set; }
        public string CONTRASENA { get; set; }
        public string URL_SITIO { get; set; }
        public string BUSCANDO { get; set; }
        public int CANTIDAD_OFERTAS { get; set; }
        public int CANTIDAD_CLICK { get; set; }
        public string FOTO { get; set; }

        public int Loguear()
        {
            try
            {
                using (COMANDO = new SqlCommand("select id from ofertante where" +
                    " email=@email and contrasena=@contrasena", CONEXION))
                {
                    CONEXION.Open();
                    COMANDO.Parameters.AddWithValue("@email", EMAIL);
                    COMANDO.Parameters.AddWithValue("@contrasena", CONTRASENA);

                    LECTOR = COMANDO.ExecuteReader();
                    while (LECTOR.Read())
                    {
                        return (int)LECTOR["Id"];
                    }
                    return 0;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
        public (string, bool) Insertar()
        {
            try
            {
                using (COMANDO = new SqlCommand("insert into Ofertante(nombre, apellido, email, contrasena, url_sitio) " +
                    "values (@nombre, @apellido, @email, @contrasena, @url_sitio)", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@nombre", NOMBRE.Trim());
                    COMANDO.Parameters.AddWithValue("@apellido", APELLIDO.Trim());
                    COMANDO.Parameters.AddWithValue("@email", EMAIL.Trim());
                    COMANDO.Parameters.AddWithValue("@contrasena", CONTRASENA.Trim());
                    COMANDO.Parameters.AddWithValue("@url_sitio", URL_SITIO != null && URL_SITIO!= string.Empty? URL_SITIO.Trim() : DBNull.Value);
                    CONEXION.Open();
                    COMANDO.ExecuteNonQuery();
                    return ("Te has registrado con exito", true);
                }
            }
            catch (SqlException e)
            {
                return e.Number == 2627 ? ("Ya existe un usuario con ese email", false) : (e.Message, false);
            }
            catch (Exception e)
            {
                return (e.Message, false);
            }
        }

        public override Entidades ObtenerEntidad(int id)
        {
            try
            {
                using (COMANDO = new SqlCommand("select nombre, apellido, email, contrasena, url_sitio, buscando," +
                    "cantidad_click, foto, (select count(*) from oferta where id_ofertante=@id) cantidad_ofertas " +
                    "from ofertante where id=@id", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@id", id);
                    CONEXION.Open();
                    LECTOR = COMANDO.ExecuteReader();

                    while(LECTOR.Read())
                    {
                        NOMBRE = LECTOR["nombre"].ToString();
                        APELLIDO = LECTOR["apellido"].ToString();
                        NOMBRE_COMPLETO = NOMBRE+ ' ' + APELLIDO;
                        EMAIL = LECTOR["email"].ToString();
                        CONTRASENA = LECTOR["contrasena"].ToString();
                        URL_SITIO = LECTOR["url_sitio"].ToString();
                        BUSCANDO = LECTOR["buscando"].ToString();
                        CANTIDAD_OFERTAS = LECTOR["cantidad_ofertas"] != DBNull.Value ? 
                            (int)LECTOR["cantidad_ofertas"] : 0;
                        CANTIDAD_CLICK = LECTOR["cantidad_click"] != DBNull.Value ? 
                            (int)LECTOR["cantidad_click"] : 0;
                        FOTO = LECTOR["foto"] != DBNull.Value?
                            "data:image/jpeg;base64," + Convert.ToBase64String((byte[])LECTOR["foto"]) 
                            : "../img/fotoperfil-default.jpg";
                    }
                    return this;
                }
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public (string,bool) Actualizar()
        {
            try
            {
                using (COMANDO = new SqlCommand("update ofertante set nombre=@nombre, apellido=@apellido, email=@email," +
                    $" url_sitio=@url_sitio, buscando=@buscando{(FICHEROS.Count > 0 ? ", foto=@foto" : "")} where id=@id", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@id", ID);
                    COMANDO.Parameters.AddWithValue("@nombre", NOMBRE.Trim());
                    COMANDO.Parameters.AddWithValue("@apellido", APELLIDO.Trim());
                    COMANDO.Parameters.AddWithValue("@email", EMAIL.Trim());
                    COMANDO.Parameters.AddWithValue("@url_sitio", URL_SITIO != null && URL_SITIO != string.Empty ? URL_SITIO.Trim() : DBNull.Value);
                    COMANDO.Parameters.AddWithValue("@buscando", BUSCANDO != null && BUSCANDO != string.Empty ? BUSCANDO.Trim() : DBNull.Value);
                    if(FICHEROS.Count >0) COMANDO.Parameters.AddWithValue("@foto", ProcesarFichero());

                    CONEXION.Open();
                    COMANDO.ExecuteNonQuery();
                    return ("Informacion actualizada con exito", true);
                }
            }
            catch(SqlException e)
            {
                return e.Number == 2627 ? ("Ya existe un usuario con ese email", false) : (e.Message, false);
            }
            catch (Exception e)
            {
                return (e.Message, false); 
            }
        }

        public (string,bool) ActualizarContra(int id, string vContrasena, string nContrasena)
        {
            try
            {
                using (COMANDO = new SqlCommand("update ofertante set contrasena=@ncontrasena where contrasena=@vcontrasena" +
                    " and id=@id", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@ncontrasena", nContrasena);
                    COMANDO.Parameters.AddWithValue("@vcontrasena", vContrasena);
                    COMANDO.Parameters.AddWithValue("@id", id);
                    CONEXION.Open();                   
                    if (COMANDO.ExecuteNonQuery() > 0)
                    {                        
                        return ("Clave actualizada con exito", true);
                    }
                    return ("Esa no es tu clave actual", false);
                }
            }
            catch (SqlException e)
            {
                return (e.Message, false);
            }
            catch (Exception e)
            {
                return (e.Message, false);
            }
        }
        
    }
}
