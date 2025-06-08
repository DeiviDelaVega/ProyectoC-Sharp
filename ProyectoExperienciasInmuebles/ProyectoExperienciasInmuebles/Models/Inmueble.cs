using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoExperienciasInmuebles.Models
{
    public class Inmueble
    {
        [Display(Name = "ID Inmueble")]
        public int id_inmueble { get; set; }

        [Display(Name = "Título"), Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, ErrorMessage = "El título no debe exceder los 100 caracteres")]
        public string titulo { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres")]
        public string descripcion { get; set; }

        [Display(Name = "Dirección"), Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no debe exceder los 200 caracteres")]
        public string direccion { get; set; }

        [Display(Name = "Precio"), Required(ErrorMessage = "El precio es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal precio { get; set; }

        [Display(Name = "Imagen")]
        [StringLength(255, ErrorMessage = "La ruta de la imagen no debe exceder los 255 caracteres")]
        public string imagen { get; set; }

        [Display(Name = "Estado")]
        [StringLength(20, ErrorMessage = "El estado no debe exceder los 20 caracteres")]
        public string estado { get; set; } = "disponible";

        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecharegistro { get; set; } = DateTime.Now;
    }
}
