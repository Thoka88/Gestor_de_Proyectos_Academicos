using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAcademicoEntities
{
    internal class UsuarioCurso
    {
        public int Id_UsuarioCurso { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Curso { get; set; }

        public Curso Curso { get; set; }
    }
}
