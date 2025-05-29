using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoExperienciasInmuebles.Models;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class LoginController : Controller
    {

        DAO da = new DAO();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            var usuario = da.ValidarUsuario(correo, clave);

            if (usuario != null)
            {
                Session["ID_Usuario"] = usuario.ID_Usuario;
                Session["Rol"] = usuario.Rol;
                switch (usuario.Rol)
                {
                    case "Cliente":
                        return RedirectToAction("Index", "Cliente");
                    case "Administrador":
                        return RedirectToAction("Index", "Admin");
                    default:
                        return RedirectToAction("Login");
                }
            }
            ViewBag.Mensaje = "Correo o clave inválidos.";
            return View();
        }

        public ActionResult Registrar()
        {

            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Registrar(Cliente cli)
        {
            ViewBag.mensaje = da.RegistrarClientes(cli);
            return View(cli);
        }
    }
}