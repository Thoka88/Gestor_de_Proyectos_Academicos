using Microsoft.AspNetCore.Mvc;
using GestorAcademicoBLL;
using GestorAcademicoEntities;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string contraseña)
        {
            var user = UsuarioBLL.IniciarSesion(nombreUsuario, contraseña);

            if (user != null)
            {
                HttpContext.Session.SetString("Usuario", user.Nombre_Usuario);
                HttpContext.Session.SetString("Rol", user.Rol_Usuario);
                HttpContext.Session.SetInt32("IdUsuario", user.Id_Usuario); 

                if (user.Rol_Usuario == "Profesor")
                    return RedirectToAction("VistaProfesor", "Profesor");
                else if (user.Rol_Usuario == "Estudiante")
                    return RedirectToAction("VistaEstudiante", "Estudiante");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}