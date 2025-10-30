﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                conn.Open();
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

        public void AgregarTarea(Tarea tarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO Tareas 
                                (Titulo_Tarea, Descripcion_Tarea, Estado_Tarea, Fecha_Inicio, Fecha_Finalizacion, Id_Usuario, Id_Proyecto)
                                VALUES (@Titulo, @Descripcion, @Estado, @Inicio, @Fin, @Usuario, @Proyecto)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Titulo", tarea.Titulo_Tarea);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.Descripcion_Tarea);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea);
                cmd.Parameters.AddWithValue("@Inicio", tarea.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", tarea.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Usuario", tarea.Id_Usuario);
                cmd.Parameters.AddWithValue("@Proyecto", tarea.Id_Proyecto);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarTarea(Tarea tarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE Tareas SET 
                                Titulo_Tarea = @Titulo,
                                Descripcion_Tarea = @Descripcion,
                                Estado_Tarea = @Estado,
                                Fecha_Inicio = @Inicio,
                                Fecha_Finalizacion = @Fin
                                WHERE Id_Tarea = @IdTarea";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Titulo", tarea.Titulo_Tarea);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.Descripcion_Tarea);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado_Tarea);
                cmd.Parameters.AddWithValue("@Inicio", tarea.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", tarea.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@IdTarea", tarea.Id_Tarea);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarTarea(int idTarea)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string query = "DELETE FROM Tareas WHERE Id_Tarea = @IdTarea";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdTarea", idTarea);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
