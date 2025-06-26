using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class DAOReportes
    {
        public SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString);

        public List<ClientesMasReservas> ObtenerTop5ClientesConMasReservas()
        {
            List<ClientesMasReservas> lista = new List<ClientesMasReservas>();
            SqlCommand cmd = new SqlCommand("usp_clientesMasReservas", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ClientesMasReservas obj = new ClientesMasReservas()
                    {
                        Cliente = dr["Cliente"].ToString(),
                        CantReservas = Convert.ToInt32(dr["CantReservas"])
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

        public List<InmueblesMasReservados> ObtenerTop5InmueblesMasReservados()
{
    List<InmueblesMasReservados> lista = new List<InmueblesMasReservados>();
    SqlCommand cmd = new SqlCommand("usp_inmueblesMasReservados", cn);
    cmd.CommandType = CommandType.StoredProcedure;

    try
    {
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            InmueblesMasReservados obj = new InmueblesMasReservados()
            {
                Inmueble = dr["Inmueble"].ToString(),
                CantReservas = Convert.ToInt32(dr["CantReservas"])
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


    }
}