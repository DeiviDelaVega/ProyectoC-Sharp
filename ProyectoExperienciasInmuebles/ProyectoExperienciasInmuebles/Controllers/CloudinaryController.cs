using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ProyectoExperienciasInmuebles.Controllers
{
    public class CloudinaryController : Controller
    {
        [HttpGet]
        public ActionResult SubirImagenCloudinary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubirImagenCloudinary(HttpPostedFileBase archivo)
        {
            if (archivo != null && archivo.ContentLength > 0)
            {
                var cloudinary = CloudinaryConfig.GetInstance();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(archivo.FileName, archivo.InputStream),
                    Folder = "imagenes_mvc" // Carpeta opcional en Cloudinary
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                ViewBag.UrlImagen = uploadResult.SecureUrl.ToString();
                ViewBag.Mensaje = "Imagen subida correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Por favor, selecciona una imagen válida.";
            }

            return View();
        }
    }
}