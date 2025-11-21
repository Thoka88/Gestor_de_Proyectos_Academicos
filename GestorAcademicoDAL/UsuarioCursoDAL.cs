using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class UsuarioCursoDAL
    {
        // 🔹 1) Obtener cursos por usuario (profe o estudiante)
        public List<Curso> ObtenerCursosPorUsuario(int idUsuario)
        {
            var cursos = new List<Curso>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_UsuarioCurso_ObtenerCursosPorUsuario", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cursos.Add(new Curso
                        {
                            Id_Curso = (int)reader["Id_Curso"],
                            NombreCurso = reader["Nombre_Curso"].ToString(),
                            CodigoCurso = reader["Codigo_Curso"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }

            return cursos;
        }

        // 🔹 2) Asignar usuario a curso
        public static void AsignarUsuarioACurso(int idUsuario, int idCurso)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_UsuarioCurso_AsignarUsuarioACurso", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@Descripcion", "Se asigna el curso al usuario correspondiente.");

                cmd.ExecuteNonQuery();
            }
        }
    }
}

