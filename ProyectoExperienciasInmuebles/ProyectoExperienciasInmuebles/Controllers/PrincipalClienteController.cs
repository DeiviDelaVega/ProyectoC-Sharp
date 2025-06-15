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
        DAOReserva DAOReserva = new DAOReserva();

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

        public ActionResult DetailsReserva(int? id = null, int? p = null, string titulo = null,
                                   decimal? precioDesde = null, decimal? precioHasta = null,
                                   DateTime? fecha1 = null, DateTime? fecha2 = null,
                                   string estado = null)
        {
            if (id == null) return RedirectToAction("MenuPrincipal");

            int? idCliente = Session["ID_Cliente"] as int?;
            if (idCliente.HasValue)
            {
                DAOCliente daoCliente = new DAOCliente();
                var cliente = daoCliente.Buscar(idCliente.Value);
                ViewBag.ClienteNombreCompleto = cliente.nombre + " " + cliente.apellido;
            }

            // Guardar valores de retorno
            ViewBag.PaginaAnterior = p;
            ViewBag.Titulo = titulo;
            ViewBag.PrecioDesde = precioDesde;
            ViewBag.PrecioHasta = precioHasta;
            ViewBag.Fecha1 = fecha1?.ToString("yyyy-MM-dd");
            ViewBag.Fecha2 = fecha2?.ToString("yyyy-MM-dd");
            ViewBag.Estado = estado;

            Inmueble reg = db.ListarInmueblesTienda(null, null, null).FirstOrDefault(x => x.id_inmueble == id);
            return View(reg);
        }

        public ActionResult ReservarInmueble(int id) {

            int? idCliente = Session["ID_Cliente"] as int?;
            if (idCliente.HasValue)
            {
                DAOCliente daoCliente = new DAOCliente();
                var cliente = daoCliente.Buscar(idCliente.Value);
                ViewBag.ClienteNombreCompleto = cliente.nombre + " " + cliente.apellido;
            }

            var inmueble = db.BuscarInmueble(id);
            ViewBag.Inmueble = inmueble;
            return View(); 
        }

        [HttpPost]
        public ActionResult ReservarInmueble(int id_inmueble, DateTime fechainicio, DateTime fechafin, int pagototal)
        {
            int? idCliente = Session["ID_Cliente"] as int?;
            if (!idCliente.HasValue)
                return RedirectToAction("Login", "Acceso");

            Reserva nueva = new Reserva
            {
                IdCliente = idCliente.Value,
                IdInmueble = id_inmueble,
                fechaInicio = fechainicio,
                fechaFin = fechafin,
                estado = "pendiente",
                pagototal = pagototal
            };

            bool ok = DAOReserva.Registrar(nueva);

            if (ok)
            {
                TempData["ReservaExitosa"] = true;
                TempData["idReserva"] = nueva.IdReserva;
                return RedirectToAction("ReservarInmueble", new { id = id_inmueble });
            }



            // 🔥 SOLUCIÓN AQUÍ - volver a cargar datos del cliente para la vista
            if (idCliente.HasValue)
            {
                DAOCliente daoCliente = new DAOCliente();
                var cliente = daoCliente.Buscar(idCliente.Value);
                ViewBag.ClienteNombreCompleto = cliente.nombre + " " + cliente.apellido;
            }

            var inmueble = db.BuscarInmueble(id_inmueble);
            ViewBag.Inmueble = inmueble;

            if (ok)
                ViewBag.ReservaExitosa = true;
            else
                ViewBag.ReservaFallida = true;

            return View(nueva);
        }

        public ActionResult GenerarComprobantePDF(int idReserva)
        {
            // Buscar reserva por ID (debes tener un método DAO para eso)
            var reserva = DAOReserva.BuscarPorId(idReserva); // crea este método si no existe
            var inmueble = db.BuscarInmueble(reserva.IdInmueble);
            var cliente = new DAOCliente().Buscar(reserva.IdCliente);

            ViewBag.Inmueble = inmueble;
            ViewBag.ClienteNombreCompleto = cliente.nombre + " " + cliente.apellido;

            return new Rotativa.ViewAsPdf("ReservaPdf", reserva)
            {
                FileName = $"Reserva_{reserva.IdReserva}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait
            };
        }


    }
}

