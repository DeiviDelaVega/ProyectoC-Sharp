using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class Usuario
    {
        public int ID_Usuario { get; set; }
        public int? ID_Cliente { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
    }
}