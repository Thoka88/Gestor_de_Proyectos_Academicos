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

        public List<Proyecto> ObtenerProyectosPorCurso(int idCurso) => _dal.ObtenerProyectosPorCurso(idCurso);
        public void AgregarProyecto(Proyecto proyecto) => _dal.AgregarProyecto(proyecto);
        public void EditarProyecto(Proyecto proyecto) => _dal.EditarProyecto(proyecto);
        public void EliminarProyecto(int idProyecto) => _dal.EliminarProyecto(idProyecto);
        public List<Proyecto> ObtenerProyectosDeEstudiante(int idUsuario, int idCurso)
        {
            return _dal.ObtenerProyectosDeEstudiante(idUsuario, idCurso);
        }

    }
}
