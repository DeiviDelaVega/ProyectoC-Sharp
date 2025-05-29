using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace ProyectoExperienciasInmuebles.Models
{
    public class DAO
    {
        public string RegistrarClientes(Cliente cli) {

            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_registrarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", cli.nombre);
                cmd.Parameters.AddWithValue("@Apellido", cli.apellido);
                cmd.Parameters.AddWithValue("@Nro_Documento",cli.nroDocumento);
                cmd.Parameters.AddWithValue("@Direccion", cli.direccion);
                cmd.Parameters.AddWithValue("@Numero_Telf", cli.telefono);
                cmd.Parameters.AddWithValue("@Correo", cli.correo);
                cmd.Parameters.AddWithValue("@Clave", cli.clave);

                int i = cmd.ExecuteNonQuery();
                mensaje = $"Te registraste correctamente {i}";
            }

            return mensaje; 
        
        }

        public Usuario ValidarUsuario(string correo, string clave)
        {
            Usuario u = null;
            using (SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_validarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new Usuario
                    {
                        ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]),
                        ID_Cliente = dr["ID_Cliente"] as int?,
                        Correo = dr["Correo"].ToString(),
                        Rol = dr["Rol"].ToString()
                    };
                }
                dr.Close();
            }
            return u;
        }



    }
}