using System.Collections.Generic;
using System.Data.SqlClient;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class CursoDAL
    {
        public static List<Curso> ObtenerTodosLosCursos()
        {
            var lista = new List<Curso>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT Id_Curso, Nombre_Curso, Codigo_Curso, Descripcion FROM Cursos";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Curso
                    {
                        Id_Curso = (int)dr["Id_Curso"],
                        NombreCurso = dr["Nombre_Curso"].ToString(),
                        CodigoCurso = dr["Codigo_Curso"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
