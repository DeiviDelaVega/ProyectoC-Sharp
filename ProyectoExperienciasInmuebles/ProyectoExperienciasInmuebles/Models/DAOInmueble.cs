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
    public class DAOInmueble
    {
        public SqlConnection cn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString);
        public List<Inmueble> ListarInmueblesXFechas(DateTime? fecha1, DateTime? fecha2, string estado)
        {
            List<Inmueble> lista = new List<Inmueble>();
            SqlCommand cmd = new SqlCommand("usp_ConsultaInmueblesFechas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha1", (object)fecha1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fecha2", (object)fecha2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", (object)estado ?? DBNull.Value);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Inmueble obj = new Inmueble()
                    {
                        id_inmueble = Convert.ToInt32(dr["id_inmueble"]),
                        titulo = dr["titulo"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        direccion = dr["direccion"].ToString(),
                        precio = Convert.ToDecimal(dr["precio"]),
                        imagen = dr["imagen"].ToString(),
                        estado = dr["estado"].ToString(),
                        fecharegistro = Convert.ToDateTime(dr["fecharegistro"])
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

        public List<Inmueble> ListarInmueblesTienda(DateTime? fecha1, DateTime? fecha2, string estado)
        {
            List<Inmueble> lista = new List<Inmueble>();
            SqlCommand cmd = new SqlCommand("usp_ConsultaInmueblesFechas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha1", (object)fecha1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fecha2", (object)fecha2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", (object)estado ?? DBNull.Value);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Inmueble obj = new Inmueble()
                    {
                        id_inmueble = Convert.ToInt32(dr["id_inmueble"]),
                        titulo = dr["titulo"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        direccion = dr["direccion"].ToString(),
                        precio = Convert.ToDecimal(dr["precio"]),
                        imagen = dr["imagen"].ToString(),
                        estado = dr["estado"].ToString(),
                        fecharegistro = Convert.ToDateTime(dr["fecharegistro"])
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

        public bool Actualizar(Inmueble reg)
        {

            using (SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("usp_inmueble_actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_inmueble", reg.id_inmueble);
                    cmd.Parameters.AddWithValue("@titulo", reg.titulo);
                    cmd.Parameters.AddWithValue("@descripcion", (object)reg.descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@imagen", (object)reg.imagen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", (object)reg.estado ?? "disponible");
                    cmd.Parameters.AddWithValue("@fecharegistro", reg.fecharegistro);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cn.Close();
                    return filasAfectadas > 0;
                }
            }
        }

        public Inmueble BuscarInmueble(int id)
        {
            Inmueble reg = new Inmueble();

            using (SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_inmueble_buscarPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_inmueble", id);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Inmueble()
                    {
                        id_inmueble = dr.GetInt32(0),
                        titulo = dr.GetString(1),
                        descripcion = dr.GetString(2),
                        direccion = dr.GetString(3),
                        precio = dr.GetDecimal(4),
                        imagen = dr.GetString(5),
                        estado = dr.GetString(6),
                        fecharegistro = dr.GetDateTime(7)
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
                SqlCommand cmd = new SqlCommand("usp_inmueble_eliminar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_inmueble", id);
                bool resultado = cmd.ExecuteNonQuery() > 0;
                cn.Close();
                return resultado;
            }
        }

    }
}