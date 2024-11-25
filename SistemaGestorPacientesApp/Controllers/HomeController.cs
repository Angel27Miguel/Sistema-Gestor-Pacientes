using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Middlewares;
using SistemaGestorPacientesApp.Models;
using System.Diagnostics;

namespace SistemaGestorPacientesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
