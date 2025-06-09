using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
        public class InmuebleController : Controller
        {
            DAOInmueble db = new DAOInmueble();

        //venta



        public ActionResult ListInmuebles(DateTime? fecha1 = null, DateTime? fecha2 = null, string estado = null, int p = 0, int? startPageDisplay = null)
            {
            //Comprueba ID_USUARIO
            if (Session["ID_Usuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var inmuebles = db.ListarInmueblesXFechas(fecha1, fecha2, estado).ToList();
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

        public ActionResult Edit(int id, string fecha1 = null, string fecha2 = null, int? p = null, int? startPageDisplay = null)
        {
            Inmueble reg = db.BuscarInmueble(id);

            if (reg == null)
            {
                // Si el cliente no se encuentra, redirige a ListClientes, preservando el estado
                return RedirectToAction("ListInmuebles", new { fecha1 = fecha1, fecha2 = fecha2, p = p, startPageDisplay = startPageDisplay });
            }

            // Parámetros de estado a la vista
            ViewBag.Fecha1 = fecha1;
            ViewBag.Fecha2 = fecha2;
            ViewBag.p = p;
            ViewBag.startPageDisplay = startPageDisplay;

            return View(reg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inmueble reg, string fecha1 = null, string fecha2 = null, int? p = null, int? startPageDisplay = null)
        {
            if (ModelState.IsValid)
            {
                bool actualizado = db.Actualizar(reg);

                if (actualizado)
                {
                    ViewBag.Actualizado = true;
                }
                else
                {
                    ViewBag.Actualizado = false; // Fallo en la DB
                }
            }
            else
            {
                ViewBag.Actualizado = false; // Fallo de validación
            }

            ViewBag.Fecha1 = fecha1;
            ViewBag.Fecha2 = fecha2;
            ViewBag.p = p;
            ViewBag.startPageDisplay = startPageDisplay;
            return View(reg); // Siempre regresa la misma vista
        }

        public ActionResult Details(int? id = null, string fecha1 = null, string fecha2 = null, int? p = null, int? startPageDisplay = null)
        {
            // Verifica si el ID es nulo.
            // Si no hay ID, redirige a ListClientes, pero ahora pasando todos los parámetros recibidos.
            if (id == null)
            {
                return RedirectToAction("ListClientes", new { fecha1 = fecha1, fecha2 = fecha2, p = p, startPageDisplay = startPageDisplay });
            }

            // Busca el cliente usando el ID.
            Inmueble reg = db.BuscarInmueble(id.Value);

            // Si el cliente no se encontró en la base de datos, redirige de nuevo a ListClientes.
            // Es importante pasar los parámetros para no perder el estado.
            if (reg == null)
            {
                return RedirectToAction("ListInmuebles", new { fecha1 = fecha1, fecha2 = fecha2, p = p, startPageDisplay = startPageDisplay });
            }

            // Pasa los parámetros de fecha, página y startPageDisplay a la vista a través de ViewBag.
            // Estos valores se usarán para el botón "Retornar" en la vista de Detalles.
            ViewBag.Fecha1 = fecha1;
            ViewBag.Fecha2 = fecha2;
            ViewBag.p = p;
            ViewBag.startPageDisplay = startPageDisplay;

            // Retorna la vista de Detalles, pasando el objeto Cliente encontrado como modelo.
            return View(reg);
        }

        public ActionResult Delete(int? id = null, string fecha1 = null, string fecha2 = null, int? p = null, int? startPageDisplay = null)
        {
            if (id == null)
            {
                // Redirige a ListClientes, preservando el estado
                return RedirectToAction("ListInmuebles", new { fecha1 = fecha1, fecha2 = fecha2, p = p, startPageDisplay = startPageDisplay });
            }

            bool eliminado = db.Eliminar(id.Value);

            if (eliminado)
            {
                TempData["Eliminado"] = true;
            }

            // Redirige a ListClientes, preservando el estado
            return RedirectToAction("ListInmuebles", new { fecha1 = fecha1, fecha2 = fecha2, p = p, startPageDisplay = startPageDisplay });
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble reg)
        {
            if (ModelState.IsValid)
            {
                bool creado = db.Registrar(reg);
                if (creado)
                {
                    TempData["Mensaje"] = "Inmueble registrado correctamente";
                    return RedirectToAction("ListInmuebles");
                }
                else
                {
                    ViewBag.Error = "No se pudo registrar el inmueble.";
                }
            }

            return View(reg);
        }


    }

}
