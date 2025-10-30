using GestorAcademicoDAL;
using GestorAcademicoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAcademicoBLL
{
    public class UsuarioCursoBLL
    {
        private readonly UsuarioCursoDAL _dal = new UsuarioCursoDAL();

        public List<Curso> ObtenerCursosDeUsuario(int idUsuario)
        {
            return _dal.ObtenerCursosPorUsuario(idUsuario);
        }

    }
}
