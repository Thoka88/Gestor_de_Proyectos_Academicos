using GestorAcademicoEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestorAcademicoDAL
{
    public class UsuarioDAL
    {
        // 🔹 LOGIN
        public static Usuarios IniciarSesion(string nombreUsuario, string contrasenaHash)
        {
            Usuarios user = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_IniciarSesion", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
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

        // 🔹 Obtener estudiantes por curso
        public static List<Usuarios> ObtenerEstudiantesPorCurso(int idCurso)
        {
            var lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuarios_ObtenerEstudiantesPorCurso", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
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

        // 🔹 Obtener usuario por ID
        public static Usuarios ObtenerUsuarioPorId(int idUsuario)
        {
            Usuarios user = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_ObtenerPorId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
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

        // 🔹 Ver si existe un nombre de usuario
        public static bool ExisteNombreUsuario(string nombreUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_ExisteNombre", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // 🔹 Registrar usuario
        public static int RegistrarUsuario(Usuarios usuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_Registrar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre_Usuario);
                cmd.Parameters.AddWithValue("@Apellidos", (object?)usuario.Apellidos_Usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena_Usuario);
                cmd.Parameters.AddWithValue("@Cedula", (object?)usuario.Cedula_Usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", (object?)usuario.Correo_Usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object?)usuario.Telefono_Usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Edad", (object?)usuario.Edad_Usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdRol", usuario.Rol_Usuario);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}