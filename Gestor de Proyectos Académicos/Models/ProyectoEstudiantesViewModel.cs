using System.Collections.Generic;
using GestorAcademicoEntities;

namespace Gestor_de_Proyectos_Académicos.Models
{
    public class ProyectoEstudiantesViewModel
    {
        public int IdProyecto { get; set; }
        public int IdCurso { get; set; }
        public string NombreProyecto { get; set; }

        public List<Usuarios> EstudiantesAsignados { get; set; } = new();
        public List<Usuarios> EstudiantesDisponibles { get; set; } = new();
    }
}