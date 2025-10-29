using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProfesorController : Controller
    {
        public IActionResult VistaProfesor()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            ViewBag.Usuario = usuario;
            return View();
        }
    }
}
