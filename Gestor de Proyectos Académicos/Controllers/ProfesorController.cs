using GestorAcademicoBLL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly UsuarioCursoBLL _usuarioCursoBLL = new UsuarioCursoBLL();

        public IActionResult VistaProfesor()
        {
            
            var usuario = HttpContext.Session.GetString("Usuario");
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (usuario == null || idUsuario == null)
                return RedirectToAction("Login", "Login");

            var cursos = _usuarioCursoBLL.ObtenerCursosDeUsuario(idUsuario.Value);

            ViewBag.Usuario = usuario;
            return View(cursos);
        }
    }
}

