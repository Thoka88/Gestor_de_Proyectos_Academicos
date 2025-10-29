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

            return UsuarioDAL.IniciarSesion(nombreUsuario, contraseña);
        }
    }
}
