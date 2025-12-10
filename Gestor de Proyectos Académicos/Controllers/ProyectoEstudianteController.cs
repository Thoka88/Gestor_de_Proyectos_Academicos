using GestorAcademicoBLL;
using GestorAcademicoEntities;
using Microsoft.AspNetCore.Mvc;
using Gestor_de_Proyectos_Académicos.Models;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProyectoEstudianteController : Controller
    {
        private readonly ProyectoBLL _proyectoBLL = new ProyectoBLL();
        private readonly TareaBLL _tareaBLL = new TareaBLL();
        private readonly CursoBLL _cursoBLL = new CursoBLL();

        // ✅ Vista principal: proyectos donde el estudiante participa
        public IActionResult VistaProyectosEstudiante(int idCurso)
        {
            var rol = HttpContext.Session.GetString("Rol");
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (rol != "Estudiante" || idUsuario == null)
                return RedirectToAction("Login", "Login");

            var proyectos = _proyectoBLL.ObtenerProyectosDeEstudiante(idUsuario.Value, idCurso);
            var curso = _cursoBLL.ObtenerCursoPorId(idCurso);

            ViewBag.IdCurso = idCurso;
            ViewBag.NombreCurso = curso?.NombreCurso ?? "Sin nombre";
            return View("VistaProyectosEstudiante", proyectos);
        }

        // ✅ Detalle del proyecto (ver tareas propias)
        public IActionResult DetalleProyecto(int idProyecto, int idCurso)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            var tareas = _tareaBLL.ObtenerTareasDeEstudianteEnProyecto(idUsuario.Value, idProyecto);
            ViewBag.IdProyecto = idProyecto;
            ViewBag.IdCurso = idCurso;
            return View("DetalleProyecto", tareas);
        }

        // ✅ Agregar nueva tarea
        [HttpPost]
        public IActionResult AgregarTarea(Tarea tarea, int idCurso)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");
            if (tarea.Fecha_Inicio == default)
                tarea.Fecha_Inicio = DateTime.Now;

            if (tarea.Fecha_Finalizacion == default)
                tarea.Fecha_Finalizacion = DateTime.Now.AddDays(7);

            tarea.Id_Usuario = idUsuario.Value;
            _tareaBLL.AgregarTarea(tarea);
            TempData["TareaCreada"] = true;
            return RedirectToAction("DetalleProyecto", new { idProyecto = tarea.Id_Proyecto, idCurso });
        }

        // ✅ Editar tarea
        [HttpPost]
        public IActionResult EditarTarea(Tarea tarea, int idCurso)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");
            if (tarea.Fecha_Inicio == default)
                tarea.Fecha_Inicio = DateTime.Now;

            if (tarea.Fecha_Finalizacion == default)
                tarea.Fecha_Finalizacion = DateTime.Now.AddDays(7);

            _tareaBLL.EditarTarea(tarea);
            TempData["EstadoActualizado"] = true;
           
            return RedirectToAction("DetalleProyecto", new { idProyecto = tarea.Id_Proyecto, idCurso });
        }

        // ✅ Eliminar tarea
        [HttpPost]
        public IActionResult EliminarTarea(int idTarea, int idProyecto, int idCurso)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            _tareaBLL.EliminarTarea(idTarea);
            TempData["TareaEliminada"] = true;

            return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });
        }

        public IActionResult ReportePersonalEstudiante(int idCurso, int? idProyecto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (rol != "Estudiante" || idUsuario == null)
                return RedirectToAction("Login", "Login");

            // Todos los proyectos donde participa el estudiante en ese curso
            var proyectos = _proyectoBLL.ObtenerProyectosDeEstudiante(idUsuario.Value, idCurso);

            // 👇 Si viene idProyecto, filtramos solo ese
            if (idProyecto.HasValue)
                proyectos = proyectos
                    .Where(p => p.Id_Proyecto == idProyecto.Value)
                    .ToList();

            var items = new List<ReporteProyectoPersonalItem>();

            foreach (var p in proyectos)
            {
                var tareas = _tareaBLL.ObtenerTareasDeEstudianteEnProyecto(idUsuario.Value, p.Id_Proyecto);

                int total = tareas.Count;
                int completadas = tareas.Count(t => t.Estado_Tarea == "Completada");

                items.Add(new ReporteProyectoPersonalItem
                {
                    IdProyecto = p.Id_Proyecto,
                    NombreProyecto = p.Nombre_Proyecto,
                    TotalTareas = total,
                    TareasCompletadas = completadas,
                    Tareas = tareas
                });
            }

            var vm = new ReportePersonalViewModel
            {
                IdCurso = idCurso,
                Proyectos = items
            };

            return View("ReportePersonalEstudiante", vm);
        }


    }
}

