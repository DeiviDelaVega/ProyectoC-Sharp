using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            if (Session["ID_Usuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
    }
}