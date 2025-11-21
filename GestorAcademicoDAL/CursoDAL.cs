using System.Collections.Generic;
using System.Data;
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
            using (SqlCommand cmd = new SqlCommand("sp_Cursos_ObtenerTodos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

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

