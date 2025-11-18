using System.Collections.Generic;
using GestorAcademicoEntities;

namespace Gestor_de_Proyectos_Académicos.Models
{
    public class ReporteProyectoViewModel
    {
        public int IdProyecto { get; set; }
        public int IdCurso { get; set; }
        public string NombreProyecto { get; set; }

        public int TotalTareas { get; set; }
        public int TareasCompletadas { get; set; }

        public double PorcentajeGeneral =>
            TotalTareas == 0 ? 0 : (TareasCompletadas * 100.0 / TotalTareas);

        public List<ReporteEstudianteItem> Estudiantes { get; set; }
    }

    public class ReporteEstudianteItem
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }

        public int TotalTareas { get; set; }
        public int Completadas { get; set; }

        public double Porcentaje =>
            TotalTareas == 0 ? 0 : (Completadas * 100.0 / TotalTareas);
    }
}

