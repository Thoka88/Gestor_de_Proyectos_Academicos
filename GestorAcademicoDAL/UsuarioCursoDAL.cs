using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class UsuarioCursoDAL
    {
        

        public List<Curso> ObtenerCursosPorUsuario(int idUsuario)
        {
            var cursos = new List<Curso>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                
                string query = @"SELECT c.Id_Curso, c.Nombre_Curso, c.Codigo_Curso, c.Descripcion
                             FROM Usuarios_Cursos uc
                             INNER JOIN Cursos c ON uc.Id_Curso = c.Id_Curso
                             WHERE uc.Id_Usuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cursos.Add(new Curso
                    {
                        Id_Curso = (int)reader["Id_Curso"],
                        NombreCurso = reader["Nombre_Curso"].ToString(),
                        CodigoCurso = reader["Codigo_Curso"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        
                    });
                }
            }

            return cursos;
        }
    }
}
