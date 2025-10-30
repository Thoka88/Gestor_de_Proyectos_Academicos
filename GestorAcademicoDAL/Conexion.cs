using System.Data.SqlClient;

namespace GestorAcademicoDAL

{
    public class Conexion
    {
        private static string cadena = "Data Source=DESKTOP-KOAL0NF;Initial Catalog=DB_Software_Proyecto;Integrated Security=True";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}

