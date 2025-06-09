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

                // Si es cliente, guardar también su ID_Cliente en sesión
                if (usuario.Rol == "Cliente")
                {
                    Session["ID_Cliente"] = usuario.ID_Cliente;
                }

                switch (usuario.Rol)
                {
                    case "Cliente":
                        return RedirectToAction("MenuPrincipal", "PrincipalCliente");
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

        public ActionResult CerrarSesion()
        {
            Session.Clear(); // Limpiar sesión
            TempData["MensajeLogout"] = "Sesión cerrada con éxito.";
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Borra todas las variables de sesión
            Session.Abandon(); // Finaliza la sesión

            return RedirectToAction("Login", "Login");
        }
    }
}