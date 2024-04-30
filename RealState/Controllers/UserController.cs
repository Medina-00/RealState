
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Application.Helpers;
using Microsoft.AspNetCore.Identity;
using RealState.Infrastructure.Identity.Entities;
using RealState.Core.Application.Enums;
using Microsoft.AspNetCore.Authorization;


namespace RealState.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService , UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginView)
        {


            if (!ModelState.IsValid)
            {
                return View(loginView);
            }

            AuthenticationResponse authentication = await userService.LoginAsync(loginView , true);
            if (authentication.Activo != false)
            {

                if (authentication != null && authentication.HasError != true)
                {
                    HttpContext.Session.Set<AuthenticationResponse>("user", authentication);

                    if(authentication.Roles.Contains(Roles.Cliente.ToString()))
                    {
                        return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
                    }
                    else if(authentication.Roles.Contains(Roles.Administrador.ToString()))
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else if (authentication.Roles.Contains(Roles.Agente.ToString()))
                    {
                        return RedirectToRoute(new { controller = "Propiedad", action = "IndexAgente" });
                    }
                    else if (authentication.Roles.Contains(Roles.Desarrollador.ToString()))
                    {
                        await LogOut();
                        ViewBag.ErrorMessage = "No puedes Acceder al weBapp, solo puedes acceder a la Api.";
                        return View("ErrorUser");
                    }




                }

                loginView.HasError = authentication!.HasError;
                loginView.Error = authentication.Error;
                return View(loginView);
            }
            else if(authentication.Activo == false && authentication.Roles.Contains(Roles.Cliente.ToString()))
            {
                await LogOut();
                ViewBag.ErrorMessage = "No Puede Acceder Su Usuario Esta Inactivo, Verifique su correo y Active.";
                return View("ErrorUser");
            }
            else
            {
                await LogOut();
                ViewBag.ErrorMessage = "No Puede Acceder Su Usuario Esta Inactivo, comuniquese con el adminitrador para que lo active.";
                return View("ErrorUser");
            }



        }

        public async Task<ActionResult> ActivarNewAccount(string userId,  ActivarUser activarUser)
        {
            activarUser.Activo = "Activar";
            string response = await userService.ActivarNewAccount(userId,  activarUser);
            return View("ActivarNewAccount", response);
        }


        public async Task<ActionResult> Index(string search)
        {
           
            
             var data = await userService.GetAllUser();
             data = data.Where(d => d.Rol == Roles.Agente.ToString() && d.Activo == true);
           
            if (search != null)
            {
                return View(data.Where(d => d.Nombre.ToLower() == search.ToLower()));
            }
            return View(data);

        }


       
        public ActionResult Create()
        {
            return View(new SaveUserViewModel());
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
                
                var origin = Request.Headers["origin"];
                RegisterResponse request = await userService.RegisterAsync(saveUser, origin!);

                var user = await userManager.FindByNameAsync(saveUser.UserName);

                if(user != null)
                {
                    var updateUser = await userService.GetByUserId(user!.Id);

                    if (user != null && user.Id != null)
                    {
                        user.Foto = FileHelped.UploadFile(saveUser.File!, user.Id, false, "User");
                        updateUser.Foto = user.Foto;
                        await userService.UpdateUserAsync(user.Id, updateUser);
                    }
                }
               

                if (request.HasError)
                {
                    saveUser.HasError = request!.HasError;
                    saveUser.Error = request.Error;
                    return View(saveUser);
                }
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            catch
            {
                return View();
            }
        }
        


        [Authorize(Roles = "Agente")]
        public async Task<ActionResult> Edit(string id)
        {

            var data = await userService.GetByUserId(id);
            return View(data);
        }

        [Authorize(Roles = "Agente")]

        // POST: MejoraController/Edit/5
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
                var User = await userManager.FindByIdAsync(id);
                collection.Foto = FileHelped.UploadFile(collection.File!, id, true, "User", User!.Foto!);
                await userService.UpdateUserAsync(id, collection);

                return RedirectToRoute(new { controller = "Propiedad", action = "IndexAgente" });
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        public  async Task<IActionResult> AccessDenied()
        {
            await LogOut();

            return View();
        }

    }
}
