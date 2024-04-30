using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class PropiedadFavoritaController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropiedadService propiedadService;
        private readonly ITipoPropiedadService tipoPropiedadService;
        private readonly ITipoVentaService tipoVentaService;
        private readonly IMejoraService mejoraService;
        private readonly IPropiedadFavoritaService propiedadFavoritaService;

        public PropiedadFavoritaController(UserManager<ApplicationUser> userManager, IPropiedadService propiedadService
                                    , ITipoPropiedadService tipoPropiedadService, ITipoVentaService tipoVentaService
                        , IMejoraService mejoraService,  IPropiedadFavoritaService propiedadFavoritaService)
        {
            this.userManager = userManager;
            this.propiedadService = propiedadService;
            this.tipoPropiedadService = tipoPropiedadService;
            this.tipoVentaService = tipoVentaService;
            this.mejoraService = mejoraService;
            this.propiedadFavoritaService = propiedadFavoritaService;
        }
        public async Task<ActionResult> Index(int tipoventa, int tipopropiedad, int baños, int habitaciones, decimal precionMinimo, decimal precioMaximo, string codigo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var Favoritas = await propiedadFavoritaService.GetAllViewModel();
            var data = await propiedadService.GetAllViewModel();
            var propiedades = await propiedadService.GetAllViewModel();
            ViewBag.Propiedades = propiedades.OrderByDescending(d => d.Fecha);
            if (tipoventa != 0 && tipopropiedad != 0 && baños != 0 && habitaciones != 0 && precionMinimo != 0 && precioMaximo != 0)
            {
                ViewBag.Propiedades = data.Where(d => d.TipoVentaId == tipoventa && d.TipoPropiedadId == tipopropiedad && d.Precio >= precionMinimo && d.Precio <= precioMaximo && d.CantidadBaños == baños && d.CantidadHabitaciones == habitaciones);
            }
            else if (codigo != null)
            {
                ViewBag.Propiedades = data.Where(d => d.Numero6Digitos == codigo);
            }
            ViewBag.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVentas = await tipoVentaService.GetAllViewModel();
            return View(Favoritas.Where(d => d.UserId == currentUser!.Id));
        }

        
        public async Task<ActionResult> Create(int Id)
        {
            var currentUser = await userManager.GetUserAsync(User);

            return View(new SavePropiedadFavoritaViewModel { IdPropiedad = Id, UserId = currentUser!.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SavePropiedadFavoritaViewModel collection)
        {
            try
            {
                await propiedadFavoritaService.Add(collection);
                return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
            }
            catch (InvalidOperationException)
            {
                return View("ErrorFavorita");
            }

        }



        public async Task<ActionResult> Delete(int Id)
        {
            var data = await propiedadFavoritaService.GetByIdSaveViewModel(Id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, SavePropiedadFavoritaViewModel collection)
        {
            try
            {
                await propiedadFavoritaService.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
