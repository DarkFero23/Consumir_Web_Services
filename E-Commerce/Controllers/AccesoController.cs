using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
namespace E_Commerce.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        // Método para mostrar la vista de registro
        public ActionResult Registrar()
        {
            return View();
        }

        // Método para procesar el registro de usuario
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            try
            {
                // Verificar que las contraseñas coincidan
                if (oUsuario.contraseña == oUsuario.ConfirmarClave)
                {
                  
                    // Crear instancia del cliente del servicio web
                    ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

                    // Llamar al método del servicio web para registrar al usuario
                    string resultado = conexion.crearUsuarios(oUsuario.nombre_completo, oUsuario.correo, oUsuario.contraseña);

                    // Verificar el resultado del registro
                    if (resultado == "Usuario creado exitosamente")
                    {
                        // Redirigir a la página de inicio de sesión
                        return RedirectToAction("Login", "Acceso");
                    }
                    else
                    {
                        // Mostrar mensaje de error al usuario
                        ViewData["Mensaje"] = resultado;
                        return View();
                    }
                }
                else
                {
                    // Mostrar mensaje de error al usuario si las contraseñas no coinciden
                    ViewData["Mensaje"] = "Las contraseñas no coinciden. Inténtelo de nuevo.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                ViewData["Mensaje"] = "Error al registrar el usuario: " + ex.Message;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            try
            {
                // Crear instancia del cliente del servicio web
                ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

                // Llamar al método del servicio web para validar al usuario
                string resultado = conexion.validarUsuario(oUsuario.correo, oUsuario.contraseña);

                if (resultado == "El usuario es válido.")
                {
                    // Usuario válido, guardar en la sesión y redirigir al inicio
                    Session["usuario"] = oUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Usuario no válido, mostrar mensaje de error
                    ViewData["ErrorMessage"] = "Credenciales incorrectas.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                ViewData["ErrorMessage"] = "Error al intentar iniciar sesión: " + ex.Message;
                return View();
            }
        }
    }
}