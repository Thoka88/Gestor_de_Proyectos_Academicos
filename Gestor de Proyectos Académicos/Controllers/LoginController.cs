using GestorAcademicoBLL;
using GestorAcademicoDAL;
using GestorAcademicoEntities;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string contraseña)
        {
            var user = UsuarioBLL.IniciarSesion(nombreUsuario, contraseña);

            if (user != null)
            {
                HttpContext.Session.SetString("Usuario", user.Nombre_Usuario);
                HttpContext.Session.SetString("Rol", user.Rol_Usuario);
                HttpContext.Session.SetInt32("IdUsuario", user.Id_Usuario); 

                if (user.Rol_Usuario == "Profesor")
                    return RedirectToAction("VistaProfesor", "Profesor");
                else if (user.Rol_Usuario == "Estudiante")
                    return RedirectToAction("VistaEstudiante", "Estudiante");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }
        [HttpGet]
        public IActionResult RegistroUsuario()
        {
            // Roles básicos desde la BD o hardcodeados
            // Si tenés BLL/RolBLL mejor, pero por ahora lo simplificamos:
            ViewBag.Roles = new List<(int Id, string Nombre)>
        {
            (1, "Profesor"),
            (2, "Estudiante")
        };

            // Cursos para checkboxes
            var cursos = CursoDAL.ObtenerTodosLosCursos();
            ViewBag.Cursos = cursos;

            return View();
        }

        [HttpPost]
        public IActionResult RegistroUsuario(
            string nombreUsuario,
            string apellidos,
            string cedula,
            string correo,
            string telefono,
            int? edad,
            string contraseña,
            string idRol,
            int[] cursosSeleccionados)
        {
            var nuevo = new Usuarios
            {
                Nombre_Usuario = nombreUsuario,
                Apellidos_Usuario = apellidos,
                Cedula_Usuario = cedula,
                Correo_Usuario = correo,
                Telefono_Usuario = telefono,
                Edad_Usuario = edad,
                Contrasena_Usuario = contraseña,
                Rol_Usuario = idRol
            };

            string error;
            bool ok = UsuarioBLL.RegistrarUsuarioConCursos(nuevo, cursosSeleccionados, out error);

            if (!ok)
            {
                ViewBag.Error = error;

                // volvemos a cargar roles y cursos por si hay error
                ViewBag.Roles = new List<(int Id, string Nombre)>
            {
                (1, "Estudiante"),
                (2, "Profesor")
            };
                ViewBag.Cursos = CursoDAL.ObtenerTodosLosCursos();

                return View();
            }

            TempData["Mensaje"] = "Registro completado. Ahora podés iniciar sesión.";
            return RedirectToAction("Login");
        }
    }
}