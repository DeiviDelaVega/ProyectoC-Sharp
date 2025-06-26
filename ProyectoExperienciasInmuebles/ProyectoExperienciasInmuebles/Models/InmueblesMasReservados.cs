using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoExperienciasInmuebles.Models
{
    public class InmueblesMasReservados
    {
        public string Inmueble { get; set; }

        [Display(Name = "Cantidad de Reservas")]
        public int CantReservas { get; set; }
    }
}