using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Application.ViewModel.PropiedadMejora;

namespace RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MejoraController : Controller
    {
        private readonly IMejoraService mejoraService;

        public MejoraController(IMejoraService mejoraService)
        {
            this.mejoraService = mejoraService;
        }
        public async Task<ActionResult> Index()
        {
            var data = await mejoraService.GetAllViewModel();
            return View(data);
        }

        

        public ActionResult Create()
        {
            return View(new SaveMejoraViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveMejoraViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                await mejoraService.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var data = await mejoraService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SaveMejoraViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                await mejoraService.Update(collection, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var data = await mejoraService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveMejoraViewModel collection)
        {
            try
            {
                await mejoraService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
