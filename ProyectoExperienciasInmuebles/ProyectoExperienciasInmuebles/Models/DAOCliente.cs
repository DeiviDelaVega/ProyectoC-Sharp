using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ProyectoExperienciasInmuebles.Models
{
    public class DAOCliente
    {
        public SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString);

        public List<Cliente> ListarClientesXFechas(DateTime? fecha1, DateTime? fecha2, int idCliente)
        {
            List<Cliente> lista = new List<Cliente>();
            SqlCommand cmd = new SqlCommand("usp_ConsultaClienteFechas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha1", (object)fecha1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fecha2", (object)fecha2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id_cliente", idCliente);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente obj = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        apellido = dr[2].ToString(),
                        nroDocumento = dr[3].ToString(),
                        direccion = dr[4].ToString(),
                        telefono = dr[5].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[6].ToString()),
                        correo = dr[7].ToString(),
                        clave = dr[8].ToString(),
                    };
                    lista.Add(obj);
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public bool Actualizar(Cliente reg)
        {
            using (SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("usp_cliente_actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", reg.IdCliente);
                    cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                    cmd.Parameters.AddWithValue("@apellido", reg.apellido);
                    cmd.Parameters.AddWithValue("@nro_documento", reg.nroDocumento);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@numero_telf", reg.telefono);
                    cmd.Parameters.AddWithValue("@fecharegistro", reg.fechaRegistro);
                    cmd.Parameters.AddWithValue("@correo", reg.correo);
                    cmd.Parameters.AddWithValue("@clave", reg.clave);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cn.Close();
                    return filasAfectadas > 0;
                }
            }
        }

        public Cliente Buscar(int id)
        {
            Cliente reg = new Cliente();

            using (SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_cliente_buscarPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cliente", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Cliente()
                    {
                        IdCliente = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        apellido = dr.GetString(2),
                        nroDocumento = dr.GetString(3),
                        direccion = dr.GetString(4),
                        telefono = dr.GetString(5),
                        fechaRegistro = dr.GetDateTime(6),
                        correo = dr.GetString(7),
                        clave = dr.GetString(8),
                    };
                }
                dr.Close();
            }
            return reg;
        }

        public bool Eliminar(int id)
        {
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_cliente_usuario_eliminar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cliente", id);
                bool resultado = cmd.ExecuteNonQuery() > 0;
                cn.Close();
                return resultado;
            }
        }


    }
}