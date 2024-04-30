using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.TipoPropiedad;

namespace RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoPropiedadController : Controller
    {
        private readonly ITipoPropiedadService tipoPropiedadService;

        public TipoPropiedadController(ITipoPropiedadService tipoPropiedadService)
        {
            this.tipoPropiedadService = tipoPropiedadService;
        }
        public async Task<ActionResult> Index()
        {
            var data = await tipoPropiedadService.GetAllViewModel();
            return View(data);
        }

       

        public ActionResult Create()
        {
            return View(new SaveTipoPropiedadViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveTipoPropiedadViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                await tipoPropiedadService.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var data = await tipoPropiedadService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SaveTipoPropiedadViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                await tipoPropiedadService.Update(collection,collection.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {

            var data = await tipoPropiedadService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveTipoPropiedadViewModel collection)
        {
            try
            {
                await tipoPropiedadService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
