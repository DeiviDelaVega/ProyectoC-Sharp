using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class ReservaController : Controller
    {
        DAOReserva dao = new DAOReserva();

        // GET: Reserva
        public ActionResult Index()
        {
            var reservas = dao.Listar();
            return View(reservas); // Vista: /Views/Reserva/Index.cshtml
        }

        // GET: Reserva/Detalles/5
        public ActionResult Detalles(int id)
        {
            var reserva = dao.BuscarPorId(id);
            if (reserva == null) return HttpNotFound();
            return View(reserva); // Vista: /Views/Reserva/Detalles.cshtml
        }

        // GET: Reserva/Crear
        public ActionResult Crear()
        {
            return View(); // Vista: /Views/Reserva/Crear.cshtml
        }

        // POST: Reserva/Crear
        [HttpPost]
        public ActionResult Crear(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                dao.Registrar(reserva);
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reserva/Editar/5
        public ActionResult Editar(int id)
        {
            var reserva = dao.BuscarPorId(id);
            if (reserva == null) return HttpNotFound();
            return View(reserva); // Vista: /Views/Reserva/Editar.cshtml
        }

        // POST: Reserva/Editar/5
        [HttpPost]
        public ActionResult Editar(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                dao.Actualizar(reserva);
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reserva/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var reserva = dao.BuscarPorId(id);
            if (reserva == null) return HttpNotFound();
            return View(reserva); // Vista: /Views/Reserva/Eliminar.cshtml
        }

        // POST: Reserva/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmado(int id)
        {
            dao.Eliminar(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExportarExcel()
        {
            var reservas = dao.Listar();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reservas");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "ID Reserva";
                worksheet.Cell(currentRow, 2).Value = "ID Cliente";
                worksheet.Cell(currentRow, 3).Value = "ID Inmueble";
                worksheet.Cell(currentRow, 4).Value = "Fecha Inicio";
                worksheet.Cell(currentRow, 5).Value = "Fecha Fin";
                worksheet.Cell(currentRow, 6).Value = "Estado";
                worksheet.Cell(currentRow, 7).Value = "Fecha Reserva";
                worksheet.Cell(currentRow, 8).Value = "Pago Total";

                foreach (var r in reservas)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = r.IdReserva;
                    worksheet.Cell(currentRow, 2).Value = r.IdCliente;
                    worksheet.Cell(currentRow, 3).Value = r.IdInmueble;
                    worksheet.Cell(currentRow, 4).Value = r.fechaInicio.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 5).Value = r.fechaFin.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 6).Value = r.estado;
                    worksheet.Cell(currentRow, 7).Value = r.fechaReserva.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 8).Value = r.pagototal;
                }

                var rango = worksheet.Range(1, 1, currentRow, 8);
                var tabla = rango.CreateTable();

                tabla.Theme = XLTableTheme.TableStyleMedium1; 

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Reservas.xlsx");
                }
            }
        }
    }
}