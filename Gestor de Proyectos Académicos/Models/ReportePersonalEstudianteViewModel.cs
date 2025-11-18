using GestorAcademicoEntities;
using System.Collections.Generic;

namespace Gestor_de_Proyectos_Académicos.Models
{
    public class ReportePersonalViewModel
    {
        public int IdCurso { get; set; }
        public List<ReporteProyectoPersonalItem> Proyectos { get; set; } = new();
    }

    public class ReporteProyectoPersonalItem
    {
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }

        public int TotalTareas { get; set; }
        public int TareasCompletadas { get; set; }
        public List<Tarea> Tareas { get; set; } = new();

        public double Porcentaje =>
            TotalTareas == 0 ? 0 : (TareasCompletadas * 100.0 / TotalTareas);
    }
}


