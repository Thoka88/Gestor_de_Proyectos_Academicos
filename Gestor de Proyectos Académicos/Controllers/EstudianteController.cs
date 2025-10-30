using GestorAcademicoBLL;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly UsuarioCursoBLL _usuarioCursoBLL = new UsuarioCursoBLL();

        public IActionResult VistaEstudiante()
        {
            // 👇 Leer las mismas claves de sesión que en el LoginController
            var usuario = HttpContext.Session.GetString("Usuario");
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (usuario == null || idUsuario == null)
                return RedirectToAction("Login", "Login");

            var cursos = _usuarioCursoBLL.ObtenerCursosDeUsuario(idUsuario.Value);

            ViewBag.Usuario = usuario;
            return View(cursos); // Pasamos los cursos a la vista
        }
    }
}
