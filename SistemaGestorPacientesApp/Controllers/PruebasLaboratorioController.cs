using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Middlewares;
using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using System.Diagnostics;
using System.Threading.Tasks;
using SistemaGestorPacientesApp.Models;
using SistemaGestorPacientesApp.Core.Application.Services;

namespace SistemaGestorPacientesApp.Controllers
{
    public class PruebasLaboratorioController : Controller
    {
        private readonly IPruebaLaboratorioService _pruebaService;
        private readonly IConsultorioService _consultorioService;
        private readonly ILogger<PruebasLaboratorioController> _logger;
        private readonly ValidateUserSession _validateUserSession;

        public PruebasLaboratorioController(ILogger<PruebasLaboratorioController> logger, ValidateUserSession validateUserSession, IPruebaLaboratorioService pruebaService, IConsultorioService consultorioService)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _pruebaService = pruebaService;
            _consultorioService = consultorioService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var pruebas = await _pruebaService.GetAllViewModel();
            return View(pruebas);
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SavePruebaLaboratorioViewModel vm = new();
            vm.Consultorios = await _consultorioService.GetAllViewModel();
            return View("SavePruebaLaboratorio", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePruebaLaboratorioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorios = await _consultorioService.GetAllViewModel();
                return View("SavePruebaLaboratorio", vm);
            }

            SavePruebaLaboratorioViewModel medicoVm = await _pruebaService.Add(vm);

            return RedirectToRoute(new { controller = "PruebasLaboratorio", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePruebaLaboratorioViewModel vm = await _pruebaService.GetByIdSaveViewModel(id);
            vm.Consultorios = await _consultorioService.GetAllViewModel();
            return View("SavePruebaLaboratorio", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePruebaLaboratorioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorios = await _consultorioService.GetAllViewModel();
                return View("SaveMedico", vm);
            }

            SavePruebaLaboratorioViewModel medicoVm = await _pruebaService.GetByIdSaveViewModel(vm.Id);
            await _pruebaService.Update(vm);
            return RedirectToRoute(new { controller = "PruebasLaboratorio", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _pruebaService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _pruebaService.Delete(id);
            return RedirectToRoute(new { controller = "PruebasLaboratorio", action = "Index" });
        }
    }
}
