using GestorAcademicoDAL;
using GestorAcademicoEntities;

public class CursoBLL
{
    public Curso ObtenerCursoPorId(int idCurso)
    {
        return CursoDAL.ObtenerCursoPorId(idCurso);
    }
}

