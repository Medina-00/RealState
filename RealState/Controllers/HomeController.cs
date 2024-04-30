using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces.Services;


namespace RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropiedadService propiedadService;
        private readonly IUserService userService;

        public HomeController(IPropiedadService propiedadService, IUserService userService , ILogger<HomeController> logger)
        {
            this.propiedadService = propiedadService;
            this.userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var users = await userService.GetAllUser();
            var propiedades = await propiedadService.GetAllViewModel();
            ViewBag.Propiedades = propiedades.Count();
            ViewBag.ClientesActivos = users.Where(u => u.Rol == Roles.Cliente.ToString() && u.Activo == true).Count();
            ViewBag.ClientesInactivos = users.Where(u => u.Rol == Roles.Cliente.ToString() && u.Activo == false).Count();
            ViewBag.AgentesActivos = users.Where(u => u.Rol == Roles.Agente.ToString() && u.Activo == true).Count();
            ViewBag.AgentesInactivos = users.Where(u => u.Rol == Roles.Agente.ToString() && u.Activo == false).Count();
            ViewBag.DesarrolladoresActivos = users.Where(u => u.Rol == Roles.Desarrollador.ToString() && u.Activo == true).Count();
            ViewBag.DesarrolladoresInactivos = users.Where(u => u.Rol == Roles.Desarrollador.ToString() && u.Activo == false).Count();
            return View();
        }

        
    }
}
