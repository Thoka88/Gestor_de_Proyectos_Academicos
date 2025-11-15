using Gestor_de_Proyectos_Académicos.Models;
using GestorAcademicoBLL;
using GestorAcademicoEntities;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class ProyectoProfesorController : Controller
    {
        private readonly ProyectoBLL _proyectoBLL = new ProyectoBLL();
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();
        private readonly TareaBLL _tareaBLL = new TareaBLL();



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
            proyecto.Estado_Proyecto = "Pendiente";

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
        [HttpPost]
        public IActionResult AgregarEstudianteProyecto(int idProyecto, int idCurso, int idUsuario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            // Si por alguna razón no se seleccionó estudiante, solo recargamos
            if (idUsuario <= 0)
                return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });

            _proyectoBLL.AsignarEstudianteAProyecto(idProyecto, idUsuario);

            return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });
        }

        [HttpPost]
        public IActionResult EliminarEstudianteProyecto(int idProyecto, int idCurso, int idUsuario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            if (idUsuario <= 0)
            {
                // Por si algo raro viene de la vista
                return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });
            }

            _proyectoBLL.EliminarEstudianteDeProyecto(idProyecto, idUsuario);

            return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });
        }
        [HttpGet]
        public IActionResult CrearTareaEnProyecto(int idProyecto, int idCurso, int idUsuario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            // Podés pasar datos por ViewBag para mostrar algo en la vista
            ViewBag.IdProyecto = idProyecto;
            ViewBag.IdCurso = idCurso;
            ViewBag.IdUsuario = idUsuario;

            return View(); // Vista: Views/ProyectoProfesor/CrearTareaEnProyecto.cshtml
        }
        [HttpPost]
        public IActionResult CrearTareaEnProyecto(int idProyecto, int idCurso, int idUsuario,
                                          string titulo, string descripcion,
                                          DateTime fechaInicio, DateTime fechaFin)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            var tarea = new Tarea
            {
                Titulo_Tarea = titulo,
                Descripcion_Tarea = descripcion,
                Estado_Tarea = "Pendiente",
                Fecha_Inicio = fechaInicio,
                Fecha_Finalizacion = fechaFin,
                Id_Usuario = idUsuario,
                Id_Proyecto = idProyecto
            };

            _tareaBLL.AgregarTarea(tarea);

            return RedirectToAction("DetalleProyecto", new { idProyecto, idCurso });
        }
        public IActionResult VerTareasEstudiante(int idProyecto, int idCurso, int idUsuario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            // Tareas del estudiante en ese proyecto (ya estás usando algo parecido en el controller del estudiante)
            var tareas = _tareaBLL.ObtenerTareasDeEstudianteEnProyecto(idUsuario, idProyecto);

            // Info del estudiante (si tenés un método así; si no, omitimos el nombre)
            var estudiante = _usuarioBLL.ObtenerUsuarioPorId(idUsuario);


            // Info del proyecto (para mostrar nombre en el título)
            var proyecto = _proyectoBLL.ObtenerProyectoPorId(idProyecto);

            var vm = new TareasEstudianteProyectoViewModel
            {
                IdProyecto = idProyecto,
                IdCurso = idCurso,
                IdUsuario = idUsuario,
                NombreEstudiante = estudiante?.Nombre_Usuario,
                NombreProyecto = proyecto?.Nombre_Proyecto,
                Tareas = tareas
            };

            return View("VerTareasEstudiante", vm);
        }




        // ✅ Detalle del proyecto (opcional, para asignar estudiantes)
        public IActionResult DetalleProyecto(int idProyecto, int idCurso)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Profesor")
                return RedirectToAction("Login", "Login");

            // 1. Obtener datos del proyecto
            var proyecto = _proyectoBLL.ObtenerProyectoPorId(idProyecto);

            // 2. Obtener estudiantes asignados a este proyecto
            var estudiantesAsignados = _proyectoBLL.ObtenerEstudiantesPorProyecto(idProyecto);

            // 3. Obtener todos los estudiantes del curso
            var estudiantesDelCurso = _usuarioBLL.ObtenerEstudiantesPorCurso(idCurso);

            // 4. Filtrar estudiantes NO asignados
            var estudiantesDisponibles = estudiantesDelCurso
                .Where(e => !estudiantesAsignados.Any(a => a.Id_Usuario == e.Id_Usuario))
                .ToList();

            // 5. Construir el ViewModel
            var vm = new ProyectoEstudiantesViewModel
            {
                IdProyecto = idProyecto,
                IdCurso = idCurso,
                NombreProyecto = proyecto.Nombre_Proyecto,
                EstudiantesAsignados = estudiantesAsignados,
                EstudiantesDisponibles = estudiantesDisponibles
            };

            // 6. Retornar la vista con el modelo
            return View("DetalleProyecto", vm);
        }

    }
}

