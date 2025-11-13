using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class ProyectoDAL
    {
        public List<Proyecto> ObtenerProyectosPorCurso(int idCurso)
        {
            var lista = new List<Proyecto>();
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
               
                string query = "SELECT * FROM Proyectos WHERE Id_Curso = @IdCurso";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Proyecto
                    {
                        Id_Proyecto = (int)dr["Id_Proyecto"],
                        Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                        Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                        Fecha_Inicio = (DateTime)dr["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)dr["Fecha_Finalizacion"],
                        Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                        Id_Curso = (int)dr["Id_Curso"]
                    });
                }
            }
            return lista;
        }

        public void AgregarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
               
                string query = @"INSERT INTO Proyectos 
                    (Nombre_Proyecto, Descripcion_Proyecto, Fecha_Inicio, Fecha_Finalizacion, Estado_Proyecto, Id_Curso)
                    VALUES (@Nombre, @Descripcion, @Inicio, @Fin, @Estado, @Curso)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre_Proyecto);
                cmd.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion_Proyecto);
                cmd.Parameters.AddWithValue("@Inicio", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", proyecto.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Estado", proyecto.Estado_Proyecto);
                cmd.Parameters.AddWithValue("@Curso", proyecto.Id_Curso);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE Proyectos 
                                 SET Nombre_Proyecto = @Nombre,
                                     Descripcion_Proyecto = @Descripcion,
                                     Fecha_Inicio = @Inicio,
                                     Fecha_Finalizacion = @Fin,
                                     Estado_Proyecto = @Estado
                                 WHERE Id_Proyecto = @IdProyecto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre_Proyecto);
                cmd.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion_Proyecto);
                cmd.Parameters.AddWithValue("@Inicio", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", proyecto.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Estado", proyecto.Estado_Proyecto);
                cmd.Parameters.AddWithValue("@IdProyecto", proyecto.Id_Proyecto);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProyecto(int idProyecto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string query = "DELETE FROM Proyectos WHERE Id_Proyecto = @IdProyecto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Proyecto> ObtenerProyectosDeEstudiante(int idUsuario, int idCurso)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                

                string query = @"
            SELECT p.*
            FROM Proyectos p
            INNER JOIN Tareas t ON p.Id_Proyecto = t.Id_Proyecto
            WHERE t.Id_Usuario = @IdUsuario AND p.Id_Curso = @IdCurso
            GROUP BY p.Id_Proyecto, p.Nombre_Proyecto, p.Descripcion_Proyecto, 
                     p.Fecha_Inicio, p.Fecha_Finalizacion, p.Estado_Proyecto, p.Id_Curso";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Proyecto
                    {
                        Id_Proyecto = (int)dr["Id_Proyecto"],
                        Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                        Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                        Fecha_Inicio = (DateTime)dr["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)dr["Fecha_Finalizacion"],
                        Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                        Id_Curso = (int)dr["Id_Curso"]
                    });
                }
            }

            return lista;
        }
    }
}
