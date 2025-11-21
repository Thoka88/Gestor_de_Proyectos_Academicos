using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GestorAcademicoEntities;

namespace GestorAcademicoDAL
{
    public class TareaDAL
    {
        // 🔹 1) Tareas de un estudiante en un proyecto
        public List<Tarea> ObtenerTareasDeEstudianteEnProyecto(int idUsuario, int idProyecto)
        {
            var lista = new List<Tarea>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Tareas_ObtenerDeEstudianteEnProyecto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
            }

            return lista;
        }

        // 🔹 2) Todas las tareas de un proyecto (para reportes del profe)
        public List<Tarea> ObtenerTareasPorProyecto(int idProyecto)
        {
            var lista = new List<Tarea>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Tareas_ObtenerPorProyecto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
            }

            return lista;
        }

        // 🔹 3) Agregar tarea
        public void AgregarTarea(Tarea tarea)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Tarea_Agregar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Titulo", tarea.Titulo_Tarea);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)tarea.Descripcion_Tarea ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea ?? "Pendiente");

                // Evitar fechas 0001-01-01
                var fechaInicio = tarea.Fecha_Inicio == default ? DateTime.Now : tarea.Fecha_Inicio;
                var fechaFin = tarea.Fecha_Finalizacion == default ? DateTime.Now.AddDays(7) : tarea.Fecha_Finalizacion;

                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                cmd.Parameters.AddWithValue("@IdUsuario", tarea.Id_Usuario);
                cmd.Parameters.AddWithValue("@IdProyecto", tarea.Id_Proyecto);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 4) Editar tarea (solo estado)
        public void EditarTarea(Tarea tarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Tarea_EditarEstado", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdTarea", tarea.Id_Tarea);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea ?? "Pendiente");

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 5) Eliminar tarea
        public void EliminarTarea(int idTarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Tarea_Eliminar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTarea", idTarea);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

