using System.Collections.Generic;
using GestorAcademicoEntities;

namespace Gestor_de_Proyectos_Académicos.Models
{
    public class TareasEstudianteProyectoViewModel
    {
        public int IdProyecto { get; set; }
        public int IdCurso { get; set; }
        public int IdUsuario { get; set; }

        public string NombreProyecto { get; set; }
        public string NombreEstudiante { get; set; }

        public List<Tarea> Tareas { get; set; } = new();
    }
}

