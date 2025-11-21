namespace GestorAcademicoEntities
{
    public class Usuarios

    {
        public int Id_Usuario { get; set; }

        public string Nombre_Usuario { get; set; }
        public string Apellidos_Usuario { get; set; }

        public string Contrasena_Usuario { get; set; }

        public string Cedula_Usuario { get; set; }
        public string Correo_Usuario { get; set; }
        public string Telefono_Usuario { get; set; }

        public int? Edad_Usuario { get; set; }   // int? si en BD acepta NULL

        public string Rol_Usuario { get; set; } 
    }
}
