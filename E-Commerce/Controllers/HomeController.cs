using E_Commerce.Models;
using E_Commerce.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Actualizar()
        {

            return View();
        }
        public ActionResult Eliminar()
        {

            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");

        }
        
            [HttpPost]
            public ActionResult Actualizar(Usuario oUsuario)
            {
                try
                {
                    // Crear instancia del cliente del servicio web
                    ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

                    // Llamar al método del servicio web para actualizar al usuario
                    string resultado = conexion.actualizarUsuario(oUsuario.dni, oUsuario.correo, oUsuario.nombre_completo, oUsuario.contraseña);

                    // Extraer el texto del XML
                    var doc = new System.Xml.XmlDocument();
                    doc.LoadXml(resultado);
                    string textoResultado = doc.InnerText;

                    Console.WriteLine("Respuesta del servicio: " + textoResultado);

                    if (textoResultado == "Usuario Actualizado")
                    {
                        Console.WriteLine("Usuario actualizado correctamente.");
                        // Redirigir a una página de éxito
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        // Mostrar mensaje de error
                        ViewData["Mensaje"] = textoResultado;
                        return View("Actualizar"); // Cambiar "About" por "Actualizar" si es necesario
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones
                    ViewData["Mensaje"] = "Error al actualizar el usuario: " + ex.Message;
                    return View("Actualizar"); // Cambiar "About" por "Actualizar" si es necesario
                }
            }
        [HttpPost]
        public ActionResult Eliminar(Usuario oUsuario)
        {
            try 
            {
                // Crear instancia del cliente del servicio web
                ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

                // Llamar al método del servicio web para eliminar al usuario
                string resultado = conexion.eliminarUsuario(oUsuario.dni);

                // Extraer el texto del XML
                var doc = new System.Xml.XmlDocument();
                doc.LoadXml(resultado);
                string textoResultado = doc.InnerText;

                Console.WriteLine("Respuesta del servicio: " + textoResultado);

                if (textoResultado == "Usuario Eliminado")
                {
                    Console.WriteLine("Usuario eliminado correctamente.");
                    // Redirigir a una página de éxito o al inicio
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Mostrar mensaje de error
                    ViewData["Mensaje"] = textoResultado;
                    return View("Eliminar"); // Cambiar "About" por "Eliminar" si es necesario
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                ViewData["Mensaje"] = "Error al eliminar el usuario: " + ex.Message;
                return View("Eliminar"); // Cambiar "About" por "Eliminar" si es necesario
            }
        }

 
    }


    }
