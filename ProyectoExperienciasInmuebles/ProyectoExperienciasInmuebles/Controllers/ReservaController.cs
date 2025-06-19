using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}