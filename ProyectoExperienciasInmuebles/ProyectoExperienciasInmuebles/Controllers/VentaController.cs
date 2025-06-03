using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class VentaController : Controller
    {
        DAOInmueble db = new DAOInmueble();

        //venta

        public ActionResult MenuPrincipal(DateTime? fecha1 = null, DateTime? fecha2 = null, string estado = null, int p = 0, int? startPageDisplay = null)
        {
            var inmuebles = db.ListarInmueblesTienda(fecha1, fecha2, estado).ToList();
            int total = inmuebles.Count();
            int filasPorPagina = 5;
            int totalPaginas = (total + filasPorPagina - 1) / filasPorPagina;

            ViewBag.Fecha1 = fecha1?.ToString("yyyy-MM-dd");
            ViewBag.Fecha2 = fecha2?.ToString("yyyy-MM-dd");
            ViewBag.Estado = estado;
            ViewBag.p = p;
            ViewBag.totalPaginas = totalPaginas;

            // --- LÓGICA PARA CONTROLAR EL RANGO VISIBLE DE PÁGINAS ---
            int pagesToShow = 5;
            int currentStartPage;

            if (startPageDisplay.HasValue)
            {
                currentStartPage = startPageDisplay.Value;
            }
            else
            {
                currentStartPage = (ViewBag.p / pagesToShow) * pagesToShow;
            }

            currentStartPage = Math.Max(0, currentStartPage);
            currentStartPage = Math.Min(currentStartPage, Math.Max(0, totalPaginas - pagesToShow));

            ViewBag.startPageDisplay = currentStartPage;
            ViewBag.pagesToShow = pagesToShow;

            var inmueblesPaginados = inmuebles.Skip(p * filasPorPagina).Take(filasPorPagina);

            return View(inmueblesPaginados);
        }


    }
}