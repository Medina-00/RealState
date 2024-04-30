using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.Propiedad;
using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Identity.Entities;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RealState.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropiedadService propiedadService;
        private readonly ITipoPropiedadService tipoPropiedadService;
        private readonly ITipoVentaService tipoVentaService;
        private readonly IMejoraService mejoraService;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraService propiedadMejoraService;
        private readonly IPropiedadFavoritaService propiedadFavoritaService;

        public PropiedadController(UserManager<ApplicationUser> userManager,IPropiedadService propiedadService
                                    ,ITipoPropiedadService tipoPropiedadService,ITipoVentaService tipoVentaService 
                        ,IMejoraService mejoraService, IUserService userService , IPropiedadMejoraService propiedadMejoraService
                        ,IPropiedadFavoritaService propiedadFavoritaService)
        {
            this.userManager = userManager;
            this.propiedadService = propiedadService;
            this.tipoPropiedadService = tipoPropiedadService;
            this.tipoVentaService = tipoVentaService;
            this.mejoraService = mejoraService;
            this.userService = userService;
            this.propiedadMejoraService = propiedadMejoraService;
            this.propiedadFavoritaService = propiedadFavoritaService;
        }
        // GET: PropiedadController
        public async Task<ActionResult> Index(int tipoventa, int tipopropiedad, int baños, int habitaciones , decimal precionMinimo, decimal precioMaximo,string codigo, string Id)
        {
            var data = await propiedadService.GetAllViewModel();
            ViewBag.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVentas = await tipoVentaService.GetAllViewModel();

            if(tipoventa != 0 && tipopropiedad != 0 && baños !=0 &&habitaciones != 0 && precionMinimo != 0 && precioMaximo != 0)
            {
                return View(data.Where(d => d.TipoVentaId == tipoventa && d.TipoPropiedadId == tipopropiedad && d.Precio >= precionMinimo && d.Precio <= precioMaximo && d.CantidadBaños == baños && d.CantidadHabitaciones == habitaciones).OrderByDescending(d => d.Fecha));
            }
            else if(codigo != null)
            {
                return View(data.Where(d=> d.Numero6Digitos == codigo));
            }
            else if (Id != null)
            {
                return View(data.Where(d => d.UserId == Id).OrderByDescending(d => d.Fecha));
            }
            return View(data.OrderByDescending(d => d.Fecha));

        }

        public async Task<ActionResult> Details(int Id)
        {
            var data = await propiedadService.GetByIdSaveViewModel(Id);
            var propiedadMejora = await propiedadMejoraService.GetAllViewModel();
            ViewBag.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVentas = await tipoVentaService.GetAllViewModel();
            ViewBag.User = await userService.GetAllUser();
            ViewBag.PropiedadMejora = propiedadMejora.Where( pm => pm.IdPropiedad == Id);
            ViewBag.Mejoras = await mejoraService.GetAllViewModel();
            return View(data);
        }

        [Authorize(Roles = "Agente")]
        public async Task<ActionResult> IndexAgente(int tipoventa, int tipopropiedad, int baños, int habitaciones, decimal precionMinimo, decimal precioMaximo, string codigo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var data = await propiedadService.GetAllViewModel();
            ViewBag.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVentas = await tipoVentaService.GetAllViewModel();
            if (tipoventa != 0 && tipopropiedad != 0 && baños != 0 && habitaciones != 0 && precionMinimo != 0 && precioMaximo != 0)
            {
                return View(data.Where(d => d.TipoVentaId == tipoventa && d.TipoPropiedadId == tipopropiedad && d.Precio >= precionMinimo && d.Precio <= precioMaximo && d.CantidadBaños == baños && d.CantidadHabitaciones == habitaciones).OrderByDescending(d => d.Fecha));
            }
            else if (codigo != null)
            {
                return View(data.Where(d => d.Numero6Digitos == codigo));
            }
            return View(data.Where(d => d.UserId == currentUser!.Id).OrderByDescending(d => d.Fecha));

        }
        [Authorize(Roles = "Agente")]

        public async Task<ActionResult> MantPropiedades(int tipoventa, int tipopropiedad, int baños, int habitaciones, decimal precionMinimo, decimal precioMaximo, string codigo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var data = await propiedadService.GetAllViewModel();
          
            ViewBag.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVentas = await tipoVentaService.GetAllViewModel();
            if (tipoventa != 0 && tipopropiedad != 0 && baños != 0 && habitaciones != 0 && precionMinimo != 0 && precioMaximo != 0)
            {
                return View(data.Where(d => d.TipoVentaId == tipoventa && d.TipoPropiedadId == tipopropiedad && d.Precio >= precionMinimo && d.Precio <= precioMaximo && d.CantidadBaños == baños && d.CantidadHabitaciones == habitaciones).OrderByDescending(d => d.Fecha).OrderByDescending(d => d.Fecha));
            }
            else if (codigo != null)
            {
                return View(data.Where(d => d.Numero6Digitos == codigo).OrderByDescending(d => d.Fecha));
            }
            return View(data.Where(d => d.UserId == currentUser!.Id).OrderByDescending(d => d.Fecha));

        }

        [Authorize(Roles = "Agente")]
        public async Task<ActionResult> Create()
        {
            var currentUser = await userManager.GetUserAsync(User);
            SavePropiedadViewModel savePropiedadViewModel = new();
            savePropiedadViewModel.UserId = currentUser!.Id;
            savePropiedadViewModel.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
            savePropiedadViewModel.TipoVenta =await tipoVentaService.GetAllViewModel();
            savePropiedadViewModel.Mejoras = await mejoraService.GetAllViewModel();
            return View(savePropiedadViewModel);
        }
        [Authorize(Roles = "Agente")]


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SavePropiedadViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(collection);
                }
                SavePropiedadViewModel SavePropiedadViewModel = await propiedadService.Add(collection);

                
                if (SavePropiedadViewModel != null && SavePropiedadViewModel.IdPropiedad != 0)
                {
                    SavePropiedadViewModel.ImagenPrincipal = FileHelped.UploadFile(collection.FilePrincipal!, SavePropiedadViewModel.IdPropiedad, false, "Propiedades");
                    SavePropiedadViewModel.Imagen1 = FileHelped.UploadFile(collection.File1!, SavePropiedadViewModel.IdPropiedad, false, "Propiedades");
                    SavePropiedadViewModel.Imagen2 = FileHelped.UploadFile(collection.File2!, SavePropiedadViewModel.IdPropiedad, false, "Propiedades");
                    SavePropiedadViewModel.Imagen3 = FileHelped.UploadFile(collection.File3!, SavePropiedadViewModel.IdPropiedad, false, "Propiedades");
                    await propiedadService.Update( SavePropiedadViewModel,SavePropiedadViewModel.IdPropiedad);
                }
                return RedirectToAction(nameof(IndexAgente));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Agente")]

        public async Task<ActionResult> Edit(int Id)
        {

            var savePropiedadViewModel = await propiedadService.GetByIdSaveViewModel(Id);
            if(savePropiedadViewModel != null)
            {
                savePropiedadViewModel.TipoVenta = await tipoVentaService.GetAllViewModel();
                savePropiedadViewModel.TipoPropiedad = await tipoPropiedadService.GetAllViewModel();
                savePropiedadViewModel.Mejoras = await mejoraService.GetAllViewModel();
                return View(savePropiedadViewModel);
            }

            return View("ErrorPropiedad");
           
           
        }

        [Authorize(Roles = "Agente")]


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, SavePropiedadViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(collection);
                }
                var propiedad = await propiedadService.GetByIdSaveViewModel(Id);
                collection.UserId = propiedad.UserId;
                collection.ImagenPrincipal = FileHelped.UploadFile(collection.FilePrincipal!, Id, true, "Propiedades", propiedad!.ImagenPrincipal!);
                collection.Imagen1 = FileHelped.UploadFile(collection.File1!, Id, true, "Propiedades", propiedad!.Imagen1!);
                collection.Imagen2 = FileHelped.UploadFile(collection.File2!, Id, true, "Propiedades", propiedad!.Imagen2!);
                collection.Imagen3 = FileHelped.UploadFile(collection.File3!, Id, true, "Propiedades", propiedad!.Imagen3!);
                await propiedadService.Update(collection, collection.IdPropiedad);

                return RedirectToAction(nameof(MantPropiedades));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Agente")]

        public async Task<ActionResult> Delete(int Id)
        {
            var data = await propiedadService.GetByIdSaveViewModel(Id);
            return View();
        }

        [Authorize(Roles = "Agente")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, IFormCollection collection)
        {
            try
            {
                await propiedadService.Delete(Id);
                FileHelped.FileDelete("Propiedades", Id);
                return RedirectToRoute(new { controller = "Propiedad", action = "IndexAgente" });
            }
            catch
            {
                return View();
            }
        }
    }
}
