using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Application.ViewModel.TipoVenta;

namespace RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoVentaController : Controller
    {
        private readonly ITipoVentaService tipoVentaService;

        public TipoVentaController(ITipoVentaService tipoVentaService)
        {
            this.tipoVentaService = tipoVentaService;
        }
        public async Task<ActionResult> Index()
        {
            var data = await tipoVentaService.GetAllViewModel();
            return View(data);
        }

        

        public ActionResult Create()
        {
            return View(new SaveTipoVentaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveTipoVentaViewModel collection)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(collection);
                }
                await tipoVentaService.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var data = await tipoVentaService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SaveTipoVentaViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                await tipoVentaService.Update(collection, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {

            var data = await tipoVentaService.GetByIdSaveViewModel(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveTipoVentaViewModel collection)
        {
            try
            {
                await tipoVentaService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
