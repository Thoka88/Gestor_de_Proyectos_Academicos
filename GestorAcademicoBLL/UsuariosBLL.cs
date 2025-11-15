using GestorAcademicoBLL.Helpers;
using GestorAcademicoDAL;
using GestorAcademicoEntities;



namespace GestorAcademicoBLL
{
    public class UsuarioBLL
    {
        public static Usuarios IniciarSesion(string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
                return null;

            string contraseñaHash = SeguridadHelper.HashSHA256(contraseña);

           
            return UsuarioDAL.IniciarSesion(nombreUsuario, contraseñaHash);


        }
        public List<Usuarios> ObtenerEstudiantesPorCurso(int idCurso)
           => UsuarioDAL.ObtenerEstudiantesPorCurso(idCurso);
        public  Usuarios ObtenerUsuarioPorId(int idUsuario)
        {
            return UsuarioDAL.ObtenerUsuarioPorId(idUsuario);
        }


    }
}
