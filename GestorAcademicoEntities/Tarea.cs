using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAcademicoEntities
{
    public class Tarea
    {
        public int Id_Tarea { get; set; }
        public string Titulo_Tarea { get; set; }
        public string Descripcion_Tarea { get; set; }
        public string Estado_Tarea { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Finalizacion { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Proyecto { get; set; }
    }

}
