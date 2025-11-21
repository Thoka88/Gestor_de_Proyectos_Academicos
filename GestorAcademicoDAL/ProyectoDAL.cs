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
        public List<Proyecto> ObtenerProyectosPorCursoDeProfesor(int idCurso, int idProfesor)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT  p.Id_Proyecto,
                            p.Nombre_Proyecto,
                            p.Descripcion_Proyecto,
                            p.Fecha_Inicio,
                            p.Fecha_Finalizacion,
                            p.Estado_Proyecto,
                            p.Id_Curso
                    FROM Proyectos p
                    INNER JOIN Proyectos_Profesores pp
                        ON p.Id_Proyecto = pp.Id_Proyecto
                    WHERE p.Id_Curso = @IdCurso
                      AND pp.Id_Usuario = @IdProfesor";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                cmd.Parameters.AddWithValue("@IdProfesor", idProfesor);

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


        public void AgregarProyecto(Proyecto proyecto, int idProfesor)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                // 1) Insertar proyecto y obtener Id_Proyecto
                string queryProyecto = @"
                    INSERT INTO Proyectos
                        (Nombre_Proyecto, Descripcion_Proyecto,
                         Fecha_Inicio, Fecha_Finalizacion,
                         Estado_Proyecto, Id_Curso)
                    OUTPUT INSERTED.Id_Proyecto
                    VALUES
                        (@Nombre, @Descripcion,
                         @Inicio, @Fin,
                         @Estado, @IdCurso)";

                SqlCommand cmd = new SqlCommand(queryProyecto, con);
                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre_Proyecto);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)proyecto.Descripcion_Proyecto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Inicio", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fin", proyecto.Fecha_Finalizacion);
                cmd.Parameters.AddWithValue("@Estado", proyecto.Estado_Proyecto ?? "Pendiente");
                cmd.Parameters.AddWithValue("@IdCurso", proyecto.Id_Curso);

                int idProyecto = (int)cmd.ExecuteScalar();   // 👈 nuevo Id_Proyecto

                // 2) Relacionar profesor con el proyecto en la tabla intermedia
                string queryPP = @"
                    INSERT INTO Proyectos_Profesores (Id_Proyecto, Id_Usuario)
                    VALUES (@IdProyecto, @IdProfesor)";

                SqlCommand cmdPP = new SqlCommand(queryPP, con);
                cmdPP.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmdPP.Parameters.AddWithValue("@IdProfesor", idProfesor);

                cmdPP.ExecuteNonQuery();
            }
        }



        public void EditarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {

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
               
                string query = "DELETE FROM Proyectos WHERE Id_Proyecto = @IdProyecto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Proyecto> ObtenerProyectosDeEstudiante(int idUsuario, int idCurso)
        {
            var lista = new List<Proyecto>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT DISTINCT 
                P.Id_Proyecto,
                P.Nombre_Proyecto,
                P.Descripcion_Proyecto,
                P.Fecha_Inicio,
                P.Fecha_Finalizacion,
                P.Estado_Proyecto,
                P.Id_Curso
            FROM Proyectos P
            INNER JOIN Proyectos_Estudiantes PE
                ON PE.Id_Proyecto = P.Id_Proyecto
            WHERE PE.Id_Usuario = @IdUsuario
              AND P.Id_Curso = @IdCurso";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                SqlDataReader dr = cmd.ExecuteReader();
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

            return lista;
        }

        public Proyecto ObtenerProyectoPorId(int idProyecto)
        {
            Proyecto proyecto = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT Id_Proyecto, Nombre_Proyecto, Descripcion_Proyecto,
                           Fecha_Inicio, Fecha_Finalizacion, Estado_Proyecto, Id_Curso
                    FROM Proyectos
                    WHERE Id_Proyecto = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", idProyecto);

                SqlDataReader dr = cmd.ExecuteReader();
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

            return proyecto;
        }
        public List<Usuarios> ObtenerEstudiantesPorProyecto(int idProyecto)
        {
            var lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT U.Id_Usuario, U.Nombre_Usuario, R.Nombre_Rol
                    FROM Proyectos_Estudiantes PE
                    INNER JOIN Usuarios U ON U.Id_Usuario = PE.Id_Usuario
                    INNER JOIN Roles_Usuarios R ON R.Id_Rol = U.Id_Rol
                    WHERE PE.Id_Proyecto = @IdProyecto";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                        Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                        Rol_Usuario = dr["Nombre_Rol"].ToString()
                    });
                }
            }

            return lista;
        }

        // 🔹 2) Asignar estudiante a proyecto (INSERT en tabla intermedia)
        public void AsignarEstudianteAProyecto(int idProyecto, int idUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    INSERT INTO Proyectos_Estudiantes (Id_Proyecto, Id_Usuario)
                    VALUES (@IdProyecto, @IdUsuario)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 3) Quitar estudiante del proyecto (DELETE en tabla intermedia)
        public void EliminarEstudianteDeProyecto(int idProyecto, int idUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            DELETE FROM Proyectos_Estudiantes
            WHERE Id_Proyecto = @IdProyecto AND Id_Usuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cmd.ExecuteNonQuery();
            }
        }
        public List<Usuarios> ObtenerEstudiantesAsignados(int idProyecto)
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT U.Id_Usuario, U.Nombre_Usuario, U.Correo_Usuario
            FROM Proyectos_Estudiantes PE
            INNER JOIN Usuarios U ON U.Id_Usuario = PE.Id_Usuario
            WHERE PE.Id_Proyecto = @IdProyecto";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                        Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                        
                    });
                }
            }

            return lista;
        }


    }
}