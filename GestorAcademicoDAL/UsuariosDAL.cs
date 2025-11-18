using GestorAcademicoEntities;
using System;
using System.Data.SqlClient;

namespace GestorAcademicoDAL
{
    public class UsuarioDAL
    {
        public static Usuarios IniciarSesion(string nombreUsuario, string contrasenaHash)
        {
            Usuarios user = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT U.Id_Usuario, U.Nombre_Usuario, U.Contrasena_Usuario, R.Nombre_Rol
            FROM Usuarios U
            INNER JOIN Roles_Usuarios R ON U.Id_Rol = R.Id_Rol
            WHERE U.Nombre_Usuario = @Nombre AND U.Contrasena_Usuario = @Contrasena";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);

                // 🔹 Aquí ahora se compara hash VS hash
                cmd.Parameters.AddWithValue("@Contrasena", contrasenaHash);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    user = new Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                        Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                        Contrasena_Usuario = dr["Contrasena_Usuario"].ToString(),
                        Rol_Usuario = dr["Nombre_Rol"].ToString()
                    };
                }
            }

            return user;
        }

        public static List<Usuarios> ObtenerEstudiantesPorCurso(int idCurso)
        {
            var lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT DISTINCT U.Id_Usuario, U.Nombre_Usuario, R.Nombre_Rol
            FROM Usuarios U
            INNER JOIN Usuarios_Cursos UC ON UC.Id_Usuario = U.Id_Usuario
            INNER JOIN Roles_Usuarios R ON R.Id_Rol = U.Id_Rol
            WHERE UC.Id_Curso = @IdCurso
              AND R.Nombre_Rol = 'Estudiante'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                        Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                        Rol_Usuario = dr["Nombre_Rol"].ToString()
                    });
                }
            }

            return lista;
        }
        public static Usuarios ObtenerUsuarioPorId(int idUsuario)
        {
            Usuarios user = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT U.Id_Usuario, U.Nombre_Usuario, U.Contrasena_Usuario, R.Nombre_Rol
            FROM Usuarios U
            INNER JOIN Roles_Usuarios R ON U.Id_Rol = R.Id_Rol
            WHERE U.Id_Usuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    user = new Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                        Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                        Contrasena_Usuario = dr["Contrasena_Usuario"].ToString(),
                        Rol_Usuario = dr["Nombre_Rol"].ToString()
                    };
                }
            }

            return user;
        }

    }
}