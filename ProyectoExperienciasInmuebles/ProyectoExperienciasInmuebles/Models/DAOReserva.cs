using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{

    public class DAOReserva
    {
        public SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ExperienciasInmueble"].ConnectionString);

        public bool Registrar(Reserva reg)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_registrar_reserva", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_cliente", reg.IdCliente);
                    cmd.Parameters.AddWithValue("@id_inmueble", reg.IdInmueble);
                    cmd.Parameters.AddWithValue("@fechainicio", reg.fechaInicio);
                    cmd.Parameters.AddWithValue("@fechafin", reg.fechaFin);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    cmd.Parameters.AddWithValue("@pagototal", reg.pagototal);

                    // Agregar parámetro OUTPUT para recibir el ID generado
                    SqlParameter idParam = new SqlParameter("@id_reserva", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idParam);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                    // Capturar el ID generado y asignarlo al objeto reserva
                    reg.IdReserva = (int)idParam.Value;

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la reserva: " + ex.Message);
            }
        }


        public Reserva BuscarPorId(int idReserva)
        {
            Reserva reserva = null;

            string sql = "SELECT * FROM reserva WHERE id_reserva = @id_reserva";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id_reserva", idReserva);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    reserva = new Reserva
                    {
                        IdReserva = (int)dr["id_reserva"],
                        IdCliente = (int)dr["id_cliente"],
                        IdInmueble = (int)dr["id_inmueble"],
                        fechaInicio = (DateTime)dr["fechainicio"],
                        fechaFin = (DateTime)dr["fechafin"],
                        estado = dr["estado"].ToString(),
                        fechaReserva = (DateTime)dr["fechareserva"],
                        pagototal = (int)dr["pagototal"]
                    };
                }

                cn.Close();
            }

            return reserva;
        }

        public List<Reserva> Listar()
        {
            List<Reserva> reservas = new List<Reserva>();

            string sql = "SELECT * FROM reserva";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Reserva r = new Reserva
                    {
                        IdReserva = (int)dr["id_reserva"],
                        IdCliente = (int)dr["id_cliente"],
                        IdInmueble = (int)dr["id_inmueble"],
                        fechaInicio = (DateTime)dr["fechainicio"],
                        fechaFin = (DateTime)dr["fechafin"],
                        estado = dr["estado"].ToString(),
                        fechaReserva = (DateTime)dr["fechareserva"],
                        pagototal = (int)dr["pagototal"]
                    };

                    reservas.Add(r);
                }

                cn.Close();
            }

            return reservas;
        }


        public bool Actualizar(Reserva reg)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_actualizar_reserva", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_reserva", reg.IdReserva);
                    cmd.Parameters.AddWithValue("@id_cliente", reg.IdCliente);
                    cmd.Parameters.AddWithValue("@id_inmueble", reg.IdInmueble);
                    cmd.Parameters.AddWithValue("@fechainicio", reg.fechaInicio);
                    cmd.Parameters.AddWithValue("@fechafin", reg.fechaFin);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    cmd.Parameters.AddWithValue("@pagototal", reg.pagototal);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva: " + ex.Message);
            }
        }

        public bool Eliminar(int idReserva)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_eliminar_reserva", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_reserva", idReserva);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva: " + ex.Message);
            }
        }


    }
}