using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class Reserva
    {
        [Display(Name = "Id Reserva")]
        public int IdReserva { get; set; }

        [Display(Name = "Id Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Id Inmueble")]
        public int IdInmueble { get; set; }

        [Display(Name = "Fecha inicio")]
        public DateTime fechaInicio { get; set; }

        [Display(Name = "Fecha fin")]
        public DateTime fechaFin { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; }

        [Display(Name = "Fecha reserva")]
        public DateTime fechaReserva { get; set; } = DateTime.Now;

        [Display(Name = "Pago Total")]
        public int pagototal { get; set; }


    }
}