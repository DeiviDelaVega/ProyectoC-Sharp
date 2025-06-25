using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class ClientesMasReservas
    {
        public string Cliente { get; set; }

        [Display(Name = "Cantidad de Reservas")]
        public int CantReservas { get; set; }
    }
}