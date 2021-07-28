using Microsoft.Data.SqlClient;
using Portal_de_empleos.Models.General;
using System;
using System.Collections.Generic;
using Portal_de_empleos.Models.General.Interfaces;

namespace Portal_de_empleos.Models
{
    public class Oferta : Entidades, IDelete
    {
        public Ofertante OFERTANTE { get; set; }
        public Categoria CATEGORIA { get; set; }
        public Provincia PROVINCIA { get; set; }
        public int ID { get; set; }        
        public string EMPRESA { get; set; }
        public string LOGO { get; set; }
        public string EMAIL_CONTACTO { get; set; }
        public string TITULO { get; set; }
        public string MODALIDAD { get; set; }
        public string TIEMPO { get; set; }
        public string DESCRIPCION { get; set; }
        public string[] REQUISITOS { get; set; }
        public decimal SUELDO { get; set; }
        public DateTime FECHAHORA { get; set; }
        public string FECHAABREVIADO { get; set; }

        public Oferta()
        {
           OFERTANTE = new Ofertante();
           CATEGORIA = new Categoria();
           PROVINCIA = new Provincia();
        }

        public override Entidades ObtenerEntidad(int id)
        {
            try
            {
                using (COMANDO = new SqlCommand("select oferta.Id, ofer.nombre + ' ' + ofer.apellido nombre_ofertante, " +
                    "Email_contacto, cat.nombre categoria, pro.nombre provincia, Empresa, logo, " +
                    "titulo, modalidad, tiempo, descripcion, requisitos, sueldo, fecha from Oferta inner join " +
                    "Ofertante ofer on Oferta.Id_Ofertante = ofer.Id inner join Categoria cat on " +
                    "Oferta.Id_Categoria = cat.Id inner join Provincia pro on " +
                    "Oferta.Id_Provincia = pro.Id where Oferta.Id=@id", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@id", id);
                    CONEXION.Open();
                    LECTOR = COMANDO.ExecuteReader();

                    while (LECTOR.Read())
                    {
                        ID = Convert.ToInt32(LECTOR["id"]);
                        OFERTANTE.NOMBRE_COMPLETO = LECTOR["nombre_ofertante"].ToString();
                        EMAIL_CONTACTO = LECTOR["email_contacto"].ToString();
                        CATEGORIA.NOMBRE = LECTOR["categoria"].ToString();
                        PROVINCIA.NOMBRE = LECTOR["provincia"].ToString();
                        EMPRESA = LECTOR["empresa"].ToString();
                        LOGO = LECTOR["logo"] != DBNull.Value ?
                            "data:image/jpeg;base64," + Convert.ToBase64String((byte[])LECTOR["logo"])
                            : "../img/buildings.png";
                        TITULO = LECTOR["titulo"].ToString();
                        DESCRIPCION = LECTOR["descripcion"].ToString();
                        MODALIDAD = LECTOR["modalidad"].ToString();
                        TIEMPO = LECTOR["tiempo"].ToString();
                        REQUISITOS = LECTOR["tiempo"].ToString().Split(",");
                        SUELDO = Convert.ToDecimal(LECTOR["sueldo"]);
                        FECHAHORA = Convert.ToDateTime(LECTOR["fecha"]);
                        FECHAABREVIADO = FECHAHORA.ToString("dd") + " " + 
                        FECHAHORA.ToString("MMMM")[0].ToString().ToUpper() +
                        FECHAHORA.ToString("MMMM").Substring(1, 2) + ", " + FECHAHORA.ToString("yyyy");
                    }
                    return this;
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

        public IEnumerable<Oferta> OfertasUsuario(int idUsuario)
        {
            List<Oferta> listado = new List<Oferta>();
            try
            {
                using (COMANDO = new SqlCommand("select oferta.Id, ofer.nombre + ' ' + ofer.apellido nombre_ofertante, " +
                    "Email_contacto, cat.nombre categoria, pro.nombre provincia, Empresa, logo, " +
                    "titulo, modalidad, tiempo, descripcion, requisitos, sueldo, fecha from Oferta inner join " +
                    "Ofertante ofer on Oferta.Id_Ofertante = ofer.Id inner join Categoria cat on " +
                    "Oferta.Id_Categoria = cat.Id inner join Provincia pro on " +
                    "Oferta.Id_Provincia = pro.Id where Oferta.Id_Ofertante=@idUsuario", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@idUsuario", idUsuario);
                    CONEXION.Open();
                    LECTOR = COMANDO.ExecuteReader();

                    while (LECTOR.Read())
                    {
                        var oferta = new Oferta();
                        oferta.ID = Convert.ToInt32(LECTOR["id"]);
                        oferta.OFERTANTE.NOMBRE_COMPLETO = LECTOR["nombre_ofertante"].ToString();
                        oferta.EMPRESA = LECTOR["empresa"].ToString();
                        oferta.EMAIL_CONTACTO = LECTOR["email_contacto"].ToString();
                        oferta.TITULO = LECTOR["titulo"].ToString();
                        oferta.CATEGORIA.NOMBRE = LECTOR["categoria"].ToString();
                        oferta.PROVINCIA.NOMBRE = LECTOR["provincia"].ToString();
                        oferta.MODALIDAD = LECTOR["modalidad"].ToString();
                        oferta.TIEMPO = LECTOR["tiempo"].ToString();
                        oferta.SUELDO = Convert.ToDecimal(LECTOR["sueldo"]);
                        oferta.LOGO = LECTOR["logo"] != DBNull.Value ?
                            "data:image/jpeg;base64,"+ Convert.ToBase64String((byte[])LECTOR["logo"])
                            : "../img/buildings.png";
                        oferta.DESCRIPCION = LECTOR["descripcion"].ToString();
                        oferta.REQUISITOS = LECTOR["requisitos"].ToString().Split(",");
                        oferta.FECHAHORA = Convert.ToDateTime(LECTOR["fecha"]);
                        oferta.FECHAABREVIADO = oferta.FECHAHORA.ToString("dd") + " " +
                        oferta.FECHAHORA.ToString("MMMM")[0].ToString().ToUpper() +
                        oferta.FECHAHORA.ToString("MMMM").Substring(1,2) + ", " + oferta.FECHAHORA.ToString("yyyy");
                        listado.Add(oferta);
                    }
                    return listado;
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

        public (string,bool) Eliminar()
        {
            try
            {
                using (COMANDO = new SqlCommand("delete from Oferta where id=@id", CONEXION))
                {
                    COMANDO.Parameters.AddWithValue("@id", ID);
                    CONEXION.Open();
                    COMANDO.ExecuteNonQuery();
                    return ("Oferta de trabajo eliminada con exito", true);
                }
            }
            catch (SqlException e) {
                return (e.Message, false);
            }
            catch (Exception e) {
                return (e.Message, false);
            }
        }
    }
}
