using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class ReportesController : Controller
    {

        DAOReportes db = new DAOReportes();

        public ActionResult ClientesConMasReservas()
        {
            var datos = db.ObtenerTop5ClientesConMasReservas();
            return View(datos);
        }
        public ActionResult InmueblesMasReservados()
        {
            var datos = db.ObtenerTop5InmueblesMasReservados();
            return View(datos);
        }
    }
}