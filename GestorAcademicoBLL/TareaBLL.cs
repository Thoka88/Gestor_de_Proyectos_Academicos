using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GestorAcademicoEntities;
using GestorAcademicoDAL;  

namespace GestorAcademicoBLL
{
    public class TareaBLL
    {
        private readonly TareaDAL _dal = new TareaDAL();

        public List<Tarea> ObtenerTareasDeEstudianteEnProyecto(int idUsuario, int idProyecto)
        {
            return _dal.ObtenerTareasDeEstudianteEnProyecto(idUsuario, idProyecto);
        }

        public void AgregarTarea(Tarea tarea)
        {
            // Si querés, podés forzar estado por defecto:
            if (string.IsNullOrEmpty(tarea.Estado_Tarea))
                tarea.Estado_Tarea = "Pendiente";

            _dal.AgregarTarea(tarea);
        }

        public void EditarTarea(Tarea tarea)
        {
            _dal.EditarTarea(tarea);
        }

        public void EliminarTarea(int idTarea)
        {
            _dal.EliminarTarea(idTarea);
        }
    }
}
