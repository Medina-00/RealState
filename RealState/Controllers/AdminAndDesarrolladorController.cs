using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Helpers;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminAndDesarrolladorController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropiedadService propiedadService;
        private readonly IAccountService accountService;

        public AdminAndDesarrolladorController(IUserService userService, UserManager<ApplicationUser> userManager, IPropiedadService propiedadService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.propiedadService = propiedadService;
        }
        public async Task<ActionResult> Index(string type)
        {
            var data = await userService.GetAllUser();
            if (type == Roles.Administrador.ToString())
            {
                ViewBag.type = "Administrador";

                return View(data.Where(d => d.Rol == Roles.Administrador.ToString()));
            }
            else if( type == Roles.Agente.ToString())
            {
                return View(data.Where(d => d.Rol == Roles.Agente.ToString()));
            }
            else
            {
                ViewBag.type = "Desarrollador";

                return View(data.Where(d => d.Rol == Roles.Desarrollador.ToString()));
            }
        }

       

        public ActionResult Create(string type)
        {
            if(type == "Administrador")
            {
                return View(new SaveUserViewModel { Rol = Roles.Administrador.ToString() });
            }
            else
            {
                return View(new SaveUserViewModel { Rol = Roles.Desarrollador.ToString() });
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveUserViewModel saveUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveUser);
                }
                RegisterResponse request = await userService.RegisterUserAdminAsync(saveUser, saveUser.UserName,false);

                var data = await userManager.FindByNameAsync(saveUser.UserName);
                var user = await userService.GetByUserId(data!.Id);
                if (request.HasError)
                {
                    saveUser.HasError = request!.HasError;
                    saveUser.Error = request.Error;
                    return View(saveUser);
                }
                else if (user.Rol == Roles.Administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Administrador" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Desarrollador" });
                }
            }
            catch
            {
                return View();
            }
        }

        public  async Task<ActionResult> Edit(string id)
        {
            var data = await userService.GetByUserId(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateUserViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }


                await userService.UpdateUserAsync(id, collection);
                var data = await userService.GetByUserId(id);
                if (data.Rol == Roles.Administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Administrador" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Desarrollador" });
                }
            }
            catch
            {
                return View();
            }
        }


        
        public ActionResult Activar(string id, string desicion)
        {
            return View(new ActivarUser { Activo = desicion });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Activar(string id, ActivarUser activarUser )
        {
            try
            {

               var user = await userService.GetByUserId(id);
                await userService.Activar(id, activarUser);
                if (user.Rol == Roles.Agente.ToString())
                {

                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Agente" });
                }
                else if(user.Rol == Roles.Administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Administrador" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Desarrollador" });
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var data = await userService.GetByUserId(id);
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, UpdateUserViewModel collection)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                var propiedades = await propiedadService.GetAllViewModel();
                var propiedadesUser = propiedades.Where(p => p.UserId == id);
                if(propiedadesUser != null)
                {
                    foreach (var item in propiedadesUser!)
                    {
                        await propiedadService.Delete(item.IdPropiedad);
                        FileHelped.FileDelete("Propiedades", item.IdPropiedad);
                    }
                }
                
                await userManager.DeleteAsync(user!);
                FileHelped.FileDelete("User", id);
                var Rol = await userService.GetByUserId(id);
                if (Rol.Rol== Roles.Administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Administrador" });
                }
                else if(Rol.Rol == Roles.Agente.ToString())
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Agente" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "AdminAndDesarrollador", action = "Index", type = "Desarrollador" });
                }
            }
            catch
            {
                return View();
            }
        }

        
        
    }
}
