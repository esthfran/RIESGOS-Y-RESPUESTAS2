using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIESGOS_Y_RESPUESTAS.Models;
using System.Diagnostics;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador,Usuario")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Usuario")]
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
