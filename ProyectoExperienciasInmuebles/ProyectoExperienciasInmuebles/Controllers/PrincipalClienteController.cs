using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class PrincipalClienteController : Controller
    {

        
            DAOInmueble db = new DAOInmueble();

            // PRINCIPALCLIENTE
            public ActionResult MenuPrincipal(
                DateTime? fecha1 = null,
                DateTime? fecha2 = null,
                string estado = null,
                string titulo = null,
                decimal? precioDesde = null,
                decimal? precioHasta = null,
                int p = 0,
                int? startPageDisplay = null)
            {
            // Obtener el nombre del cliente
            int? idCliente = Session["ID_Cliente"] as int?;
            if (idCliente.HasValue)
            {
                DAOCliente daoCliente = new DAOCliente();
                var cliente = daoCliente.Buscar(idCliente.Value);
                ViewBag.ClienteNombreCompleto = cliente.nombre + " " + cliente.apellido;
            }

                var inmuebles = db.ListarInmueblesTienda(fecha1, fecha2, estado).ToList();

                // FILTROS ADICIONALES
                if (!string.IsNullOrEmpty(titulo))
                {
                    inmuebles = inmuebles.Where(i => i.titulo != null && i.titulo.ToLower().Contains(titulo.ToLower())).ToList();
                }

                if (precioDesde.HasValue)
                {
                    inmuebles = inmuebles.Where(i => i.precio >= precioDesde.Value).ToList();
                }

                if (precioHasta.HasValue)
                {
                    inmuebles = inmuebles.Where(i => i.precio <= precioHasta.Value).ToList();
                }

                int total = inmuebles.Count();
                int filasPorPagina = 5;
                int totalPaginas = (total + filasPorPagina - 1) / filasPorPagina;

                ViewBag.Fecha1 = fecha1?.ToString("yyyy-MM-dd");
                ViewBag.Fecha2 = fecha2?.ToString("yyyy-MM-dd");
                ViewBag.Estado = estado;
                ViewBag.Titulo = titulo;
                ViewBag.PrecioDesde = precioDesde;
                ViewBag.PrecioHasta = precioHasta;
                ViewBag.p = p;
                ViewBag.totalPaginas = totalPaginas;

                // --- LÓGICA PARA CONTROLAR EL RANGO VISIBLE DE PÁGINAS ---
                int pagesToShow = 5;
                int currentStartPage = startPageDisplay ?? (p / pagesToShow) * pagesToShow;
                currentStartPage = Math.Max(0, Math.Min(currentStartPage, Math.Max(0, totalPaginas - pagesToShow)));

                ViewBag.startPageDisplay = currentStartPage;
                ViewBag.pagesToShow = pagesToShow;

                var inmueblesPaginados = inmuebles.Skip(p * filasPorPagina).Take(filasPorPagina);
                return View(inmueblesPaginados);
            }



        public ActionResult ListarPorEstado(string estado)
        {
            if (estado == "todos") estado = null;

            DAOInmueble dao = new DAOInmueble();
            var lista = dao.ListarInmueblesPorEstado(estado);
            return View(lista);
        }
    }
}

