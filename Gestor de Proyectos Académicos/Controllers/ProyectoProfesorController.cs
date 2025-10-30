using GestorAcademicoBLL;
using GestorAcademicoEntities;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProyectoProfesorController : Controller
    {
        private readonly ProyectoBLL _proyectoBLL = new ProyectoBLL();

        // ✅ Vista principal: lista de proyectos del curso
        public IActionResult VistaProyectosProfesor(int idCurso)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            var proyectos = _proyectoBLL.ObtenerProyectosPorCurso(idCurso);
            ViewBag.IdCurso = idCurso;
            ViewBag.NombreCurso = "Nombre del curso"; // opcional, si querés mostrarlo en la vista
            return View("VistaProyectosProfesor", proyectos);
        }

        // ✅ Crear nuevo proyecto
        [HttpPost]
        public IActionResult AgregarProyecto(Proyecto proyecto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            _proyectoBLL.AgregarProyecto(proyecto);
            return RedirectToAction("VistaProyectosProfesor", new { idCurso = proyecto.Id_Curso });
        }

        // ✅ Editar proyecto
        [HttpPost]
        public IActionResult EditarProyecto(Proyecto proyecto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            _proyectoBLL.EditarProyecto(proyecto);
            return RedirectToAction("VistaProyectosProfesor", new { idCurso = proyecto.Id_Curso });
        }

        // ✅ Eliminar proyecto
        [HttpPost]
        public IActionResult EliminarProyecto(int idProyecto, int idCurso)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            _proyectoBLL.EliminarProyecto(idProyecto);
            return RedirectToAction("VistaProyectosProfesor", new { idCurso });
        }

        // ✅ Detalle del proyecto (opcional, para asignar estudiantes)
        public IActionResult DetalleProyecto(int idProyecto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            ViewBag.IdProyecto = idProyecto;
            // Aquí podrías cargar los estudiantes o tareas del proyecto si querés
            return View("DetalleProyecto");
        }
    }
}

