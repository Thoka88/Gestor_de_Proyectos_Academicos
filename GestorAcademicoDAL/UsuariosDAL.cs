using GestorAcademicoEntities;
using System;
using System.Data.SqlClient;

namespace GestorAcademicoDAL
{
    public class UsuarioDAL
    {
        public static Usuarios IniciarSesion(string nombreUsuario, string contrasena)
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
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

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
