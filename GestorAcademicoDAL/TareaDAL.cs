using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class TareaDAL
    {
        public List<Tarea> ObtenerTareasDeEstudianteEnProyecto(int idUsuario, int idProyecto)
        {
            var lista = new List<Tarea>();
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM Tareas 
                                 WHERE Id_Usuario = @IdUsuario AND Id_Proyecto = @IdProyecto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Tarea
                    {
                        Id_Tarea = (int)dr["Id_Tarea"],
                        Titulo_Tarea = dr["Titulo_Tarea"].ToString(),
                        Descripcion_Tarea = dr["Descripcion_Tarea"].ToString(),
                        Estado_Tarea = dr["Estado_Tarea"].ToString(),
                        Fecha_Inicio = (DateTime)dr["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)dr["Fecha_Finalizacion"],
                        Id_Proyecto = (int)dr["Id_Proyecto"],
                        Id_Usuario = (int)dr["Id_Usuario"]
                    });
                }
            }
            return lista;
        }

        // ✅ NUEVO: todas las tareas de un proyecto (para el reporte del profesor)
        public List<Tarea> ObtenerTareasPorProyecto(int idProyecto)
        {
            var lista = new List<Tarea>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM Tareas WHERE Id_Proyecto = @IdProyecto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Tarea
                    {
                        Id_Tarea = (int)dr["Id_Tarea"],
                        Titulo_Tarea = dr["Titulo_Tarea"].ToString(),
                        Descripcion_Tarea = dr["Descripcion_Tarea"].ToString(),
                        Estado_Tarea = dr["Estado_Tarea"].ToString(),
                        Fecha_Inicio = (DateTime)dr["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)dr["Fecha_Finalizacion"],
                        Id_Proyecto = (int)dr["Id_Proyecto"],
                        Id_Usuario = (int)dr["Id_Usuario"]
                    });
                }
            }

            return lista;
        }

        public void AgregarTarea(Tarea tarea)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    INSERT INTO Tareas (Titulo_Tarea, Descripcion_Tarea, Estado_Tarea,
                                        Fecha_Inicio, Fecha_Finalizacion, Id_Usuario, Id_Proyecto)
                    VALUES (@Titulo, @Descripcion, @Estado, @FechaInicio, @FechaFin, @IdUsuario, @IdProyecto)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Titulo", tarea.Titulo_Tarea);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)tarea.Descripcion_Tarea ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea);

                // Por si acaso, evitar fechas 0001-01-01
                var fechaInicio = tarea.Fecha_Inicio == default ? DateTime.Now : tarea.Fecha_Inicio;
                var fechaFin = tarea.Fecha_Finalizacion == default ? DateTime.Now.AddDays(7) : tarea.Fecha_Finalizacion;

                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                cmd.Parameters.AddWithValue("@IdUsuario", tarea.Id_Usuario);
                cmd.Parameters.AddWithValue("@IdProyecto", tarea.Id_Proyecto);

                cmd.ExecuteNonQuery();
            }
        }

        public void EditarTarea(Tarea tarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = @"
                    UPDATE Tareas
                    SET Estado_Tarea = @Estado
                    WHERE Id_Tarea = @IdTarea";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea ?? "Pendiente");
                cmd.Parameters.AddWithValue("@IdTarea", tarea.Id_Tarea);

                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarTarea(int idTarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = "DELETE FROM Tareas WHERE Id_Tarea = @IdTarea";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdTarea", idTarea);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

