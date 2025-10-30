using GestorAcademicoBLL;
using GestorAcademicoEntities;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProyectoEstudianteController : Controller
    {
        private readonly ProyectoBLL _proyectoBLL = new ProyectoBLL();
        private readonly TareaBLL _tareaBLL = new TareaBLL();

        // ✅ Vista principal: proyectos donde el estudiante participa
        public IActionResult VistaProyectosEstudiante(int idCurso)
        {
            var rol = HttpContext.Session.GetString("Rol");
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (rol != "Estudiante" || idUsuario == null)
                return RedirectToAction("Login", "Login");

            var proyectos = _proyectoBLL.ObtenerProyectosDeEstudiante(idUsuario.Value, idCurso);
            ViewBag.IdCurso = idCurso;
            ViewBag.NombreCurso = "Nombre del curso"; // opcional
            return View("VistaProyectosEstudiante", proyectos);
        }

        // ✅ Detalle del proyecto (ver tareas propias)
        public IActionResult DetalleProyecto(int idProyecto)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            var tareas = _tareaBLL.ObtenerTareasDeEstudianteEnProyecto(idUsuario.Value, idProyecto);
            ViewBag.IdProyecto = idProyecto;
            return View("DetalleProyecto", tareas);
        }

        // ✅ Agregar nueva tarea
        [HttpPost]
        public IActionResult AgregarTarea(Tarea tarea)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            tarea.Id_Usuario = idUsuario.Value;
            _tareaBLL.AgregarTarea(tarea);
            return RedirectToAction("DetalleProyecto", new { idProyecto = tarea.Id_Proyecto });
        }

        // ✅ Editar tarea
        [HttpPost]
        public IActionResult EditarTarea(Tarea tarea)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            _tareaBLL.EditarTarea(tarea);
            return RedirectToAction("DetalleProyecto", new { idProyecto = tarea.Id_Proyecto });
        }

        // ✅ Eliminar tarea
        [HttpPost]
        public IActionResult EliminarTarea(int idTarea, int idProyecto)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            _tareaBLL.EliminarTarea(idTarea);
            return RedirectToAction("DetalleProyecto", new { idProyecto });
        }
    }
}

