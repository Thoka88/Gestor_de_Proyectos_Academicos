using GestorAcademicoEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAcademicoDAL
{
    public class ProyectoDAL
    {
        // 🔹 1) Proyectos por curso (general)
        public List<Proyecto> ObtenerProyectosPorCurso(int idCurso)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyectos_ObtenerPorCurso", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Proyecto
                        {
                            Id_Proyecto = Convert.ToInt32(dr["Id_Proyecto"]),
                            Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                            Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]),
                            Fecha_Finalizacion = Convert.ToDateTime(dr["Fecha_Finalizacion"]),
                            Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                            Id_Curso = Convert.ToInt32(dr["Id_Curso"])
                        });
                    }
                }
            }

            return lista;
        }

        // 🔹 2) Proyectos por curso filtrados por profesor
        public List<Proyecto> ObtenerProyectosPorCursoDeProfesor(int idCurso, int idProfesor)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyectos_ObtenerPorCurso_Profesor", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                cmd.Parameters.AddWithValue("@IdProfesor", idProfesor);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Proyecto
                        {
                            Id_Proyecto = Convert.ToInt32(dr["Id_Proyecto"]),
                            Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                            Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]),
                            Fecha_Finalizacion = Convert.ToDateTime(dr["Fecha_Finalizacion"]),
                            Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                            Id_Curso = Convert.ToInt32(dr["Id_Curso"])
                        });
                    }
                }
            }

            return lista;
        }

        // 🔹 3) Agregar proyecto + relacionarlo con el profesor
        public void AgregarProyecto(Proyecto proyecto, int idProfesor)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyectos_Insertar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre_Proyecto);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)proyecto.Descripcion_Proyecto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Inicio", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", proyecto.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Estado", proyecto.Estado_Proyecto ?? "Pendiente");
                cmd.Parameters.AddWithValue("@IdCurso", proyecto.Id_Curso);
                cmd.Parameters.AddWithValue("@IdProfesor", idProfesor);

                // Si querés usar el Id_Proyecto devuelto:
                var result = cmd.ExecuteScalar();
                int idProyecto = result != null ? Convert.ToInt32(result) : 0;
                // Podrías guardarlo en proyecto.Id_Proyecto si querés:
                // proyecto.Id_Proyecto = idProyecto;
            }
        }

        // 🔹 4) Editar proyecto
        public void EditarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyectos_Editar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdProyecto", proyecto.Id_Proyecto);
                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre_Proyecto);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)proyecto.Descripcion_Proyecto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Inicio", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", proyecto.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Estado", proyecto.Estado_Proyecto ?? "Pendiente");

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 5) Eliminar proyecto (ya lo tenías con SP)
        public void EliminarProyecto(int idProyecto)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_Eliminar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 6) Proyectos donde participa un estudiante (por curso)
        public List<Proyecto> ObtenerProyectosDeEstudiante(int idUsuario, int idCurso)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyectos_ObtenerPorEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Proyecto
                        {
                            Id_Proyecto = Convert.ToInt32(dr["Id_Proyecto"]),
                            Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                            Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]),
                            Fecha_Finalizacion = Convert.ToDateTime(dr["Fecha_Finalizacion"]),
                            Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                            Id_Curso = Convert.ToInt32(dr["Id_Curso"])
                        });
                    }
                }
            }

            return lista;
        }

        // 🔹 7) Obtener un proyecto por Id
        public Proyecto ObtenerProyectoPorId(int idProyecto)
        {
            Proyecto proyecto = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_ObtenerPorId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        proyecto = new Proyecto
                        {
                            Id_Proyecto = Convert.ToInt32(dr["Id_Proyecto"]),
                            Nombre_Proyecto = dr["Nombre_Proyecto"].ToString(),
                            Descripcion_Proyecto = dr["Descripcion_Proyecto"].ToString(),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]),
                            Fecha_Finalizacion = Convert.ToDateTime(dr["Fecha_Finalizacion"]),
                            Estado_Proyecto = dr["Estado_Proyecto"].ToString(),
                            Id_Curso = Convert.ToInt32(dr["Id_Curso"])
                        };
                    }
                }
            }

            return proyecto;
        }

        // 🔹 8) Estudiantes de un proyecto (con más datos si querés)
        public List<Usuarios> ObtenerEstudiantesPorProyecto(int idProyecto)
        {
            var lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_ObtenerEstudiantes", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuarios
                        {
                            Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                            Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                            // Si en el SP devolvés Nombre_Rol:
                            // Rol_Usuario = dr["Nombre_Rol"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // 🔹 9) Asignar estudiante a proyecto
        public void AsignarEstudianteAProyecto(int idProyecto, int idUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_AsignarEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 10) Quitar estudiante del proyecto
        public void EliminarEstudianteDeProyecto(int idProyecto, int idUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_QuitarEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 11) Estudiantes asignados (puede reutilizar el mismo SP)
        public List<Usuarios> ObtenerEstudiantesAsignados(int idProyecto)
        {
            var lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Proyecto_ObtenerEstudiantes", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuarios
                        {
                            Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                            Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                            // Agregás correo si lo trae el SP:
                            // Correo_Usuario = dr["Correo_Usuario"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}