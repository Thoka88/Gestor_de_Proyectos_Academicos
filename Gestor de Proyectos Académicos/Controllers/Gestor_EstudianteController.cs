using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.Controllers
{
    public class Gestor_EstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
