using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class Cliente
    {
        [Display(Name = "Id Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Nombre"), Required]
        public string nombre { get; set; }

        [Display(Name = "Apellido"), Required]
        public string apellido { get; set; }

        [Display(Name = "Documento"), Required]
        public string nroDocumento { get; set; }

        [Display(Name = "Direccion"), Required]
        public string direccion {  get; set; }

        [Display(Name = "Telefono"), Required]
        public string telefono { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Registro"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRegistro { get; set; }

        [Display(Name = "Correo"), Required]
        public string correo {  get; set; }

        [Display(Name = "Clave"), Required]
        [DataType(DataType.Password)]
        public string clave { get; set; }

    }
}