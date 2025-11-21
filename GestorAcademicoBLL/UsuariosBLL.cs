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
        public static bool RegistrarUsuarioConCursos(
    Usuarios usuario,
    int[] idsCursos,
    out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(usuario.Nombre_Usuario) ||
                string.IsNullOrWhiteSpace(usuario.Contrasena_Usuario))
            {
                mensajeError = "El usuario y la contraseña son obligatorios.";
                return false;
            }

            if (UsuarioDAL.ExisteNombreUsuario(usuario.Nombre_Usuario))
            {
                mensajeError = "Ese nombre de usuario ya existe.";
                return false;
            }

            // 🔐 Hash de contraseña
            usuario.Contrasena_Usuario =
                SeguridadHelper.HashSHA256(usuario.Contrasena_Usuario);

            // 1. Registrar usuario y obtener Id
            int idUsuario = UsuarioDAL.RegistrarUsuario(usuario);

            // 2. Asignar cursos seleccionados
            if (idsCursos != null && idsCursos.Length > 0)
            {
                foreach (int idCurso in idsCursos)
                {
                    UsuarioCursoDAL.AsignarUsuarioACurso(idUsuario, idCurso);
                }
            }

            return true;
        }


        public List<Usuarios> ObtenerEstudiantesPorCurso(int idCurso)
           => UsuarioDAL.ObtenerEstudiantesPorCurso(idCurso);
        public  Usuarios ObtenerUsuarioPorId(int idUsuario)
        {
            return UsuarioDAL.ObtenerUsuarioPorId(idUsuario);
        }


    }
}
