using GestorAcademicoDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAcademicoEntities;


namespace GestorAcademicoBLL
{
    public class ProyectoBLL
    {
        private readonly ProyectoDAL _dal = new ProyectoDAL();

        public List<Proyecto> ObtenerProyectosPorCurso(int idCurso)
            => _dal.ObtenerProyectosPorCurso(idCurso);

        public void AgregarProyecto(Proyecto proyecto)
        {
            // ✔️ Paso 2: Valor predeterminado si no viene desde la vista
            if (string.IsNullOrEmpty(proyecto.Estado_Proyecto))
                proyecto.Estado_Proyecto = "Pendiente";

            _dal.AgregarProyecto(proyecto);
        }

        public void EditarProyecto(Proyecto proyecto)
        {
            // Si querés que Editar también tenga un valor por defecto SOLO si está vacío
            if (string.IsNullOrEmpty(proyecto.Estado_Proyecto))
                proyecto.Estado_Proyecto = "Pendiente";

            _dal.EditarProyecto(proyecto);
        }

        public void EliminarProyecto(int idProyecto)
            => _dal.EliminarProyecto(idProyecto);

        public List<Proyecto> ObtenerProyectosDeEstudiante(int idUsuario, int idCurso)
            => _dal.ObtenerProyectosDeEstudiante(idUsuario, idCurso);
        public Proyecto ObtenerProyectoPorId(int idProyecto)
             => _dal.ObtenerProyectoPorId(idProyecto);

        // 🔹 Nuevo: obtener estudiantes asignados
        public List<Usuarios> ObtenerEstudiantesPorProyecto(int idProyecto)
            => _dal.ObtenerEstudiantesPorProyecto(idProyecto);

        // 🔹 Nuevo: asignar estudiante
        public void AsignarEstudianteAProyecto(int idProyecto, int idUsuario)
            => _dal.AsignarEstudianteAProyecto(idProyecto, idUsuario);

        // 🔹 Nuevo: quitar estudiante
        public void EliminarEstudianteDeProyecto(int idProyecto, int idUsuario)
            => _dal.EliminarEstudianteDeProyecto(idProyecto, idUsuario);
    }
}



