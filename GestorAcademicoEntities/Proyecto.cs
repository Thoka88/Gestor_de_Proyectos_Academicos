using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAcademicoEntities
{
    public class Proyecto
    {
        public int Id_Proyecto { get; set; }
        public string Nombre_Proyecto { get; set; }
        public string Descripcion_Proyecto { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Finalizacion { get; set; }
        public string Estado_Proyecto { get; set; }
        public int Id_Curso { get; set; }
    }
}
