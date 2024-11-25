using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Cita;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Medico;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente;
using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Middlewares;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;
        private readonly IConsultorioService _consultorioService;
        private readonly IPruebaLaboratorioService _pruebaLaboratorioService;
        private readonly IResultadoLaboratorioService _resultadoLaboratorioService;
        private readonly ILogger<CitaController> _logger;
        private readonly ValidateUserSession _validateUserSession;

        public CitaController(ILogger<CitaController> logger, ValidateUserSession validateUserSession, 
            ICitaService citaService, IMedicoService medicoService, IPacienteService pacienteService, 
            IConsultorioService consultorioService, IPruebaLaboratorioService pruebaLaboratorioService, 
            IResultadoLaboratorioService resultadoLaboratorioService
            )
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _consultorioService = consultorioService;
            _pruebaLaboratorioService = pruebaLaboratorioService;
            _resultadoLaboratorioService = resultadoLaboratorioService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var citas = await _citaService.GetAllViewModel();
            return View(citas);
        }

        public async Task<IActionResult> VerResultados(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var cita = await _citaService.GetByIdSaveViewModel(id);
            if (cita == null)
            {
                return NotFound();
            }

            return View("CompletarCita", cita); 
        }

        public async Task<IActionResult> CreateAsync()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveCitaViewModel vm = new();
            vm.Pacientes = await _pacienteService.GetAllViewModel();
            vm.Consultorios = await _consultorioService.GetAllViewModel();
            vm.Medicos = await _medicoService.GetAllViewModel();

            return View("SaveCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCitaViewModel vm)
        {
            if (!ModelState.IsValid)
            {

                vm.Pacientes = await _pacienteService.GetAllViewModel();
                vm.Consultorios = await _consultorioService.GetAllViewModel();
                vm.Medicos = await _medicoService.GetAllViewModel();
                return View("SaveCita", vm);
            }

            vm.Estado = EstadoCita.PendienteConsulta;
            await _citaService.Add(vm);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _citaService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _citaService.Delete(id);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }
        
        public async Task<IActionResult> RealizarResultado(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            
            var cita = await _citaService.GetByIdSaveViewModel(id);
            
            if (cita == null)
            {
                return NotFound();
            }
            cita.PruebasLaboratorio = await _pruebaLaboratorioService.GetAllViewModel();
            return View("RealizarResultado", cita);
        }

        [HttpPost]
        public async Task<IActionResult> RealizarResultado(SaveCitaViewModel vm)
        {
            if (vm.PruebasSeleccionadas == null || !vm.PruebasSeleccionadas.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos una prueba.");
                vm.PruebasLaboratorio = await _pruebaLaboratorioService.GetAllViewModel();
                return View("RealizarResultado", vm);
            }

            var cita = await _citaService.GetByIdSaveViewModel(vm.Id);
            if (cita == null || cita.PacienteId == 0)  
            {
                ModelState.AddModelError("", "El paciente no existe.");
                vm.PruebasLaboratorio = await _pruebaLaboratorioService.GetAllViewModel();
                return View("RealizarResultado", vm);
            }

            
            foreach (var pruebaId in vm.PruebasSeleccionadas)
            {
                await _resultadoLaboratorioService.Add(new SaveResultadoLaboratorioViewModel
                {
                    CitaId = vm.Id,
                    PruebaLaboratorioId = pruebaId,
                    EsCompletada = false,
                    PacienteId = cita.PacienteId,  
                    Fecha = DateTime.Now
                });
            }

            vm.Estado = EstadoCita.PendienteResultados;
            await _citaService.UpdateEstado(vm);

            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }


        public async Task<IActionResult> ConsultarResultados(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var resultados = await _resultadoLaboratorioService.GetByCitaId(id);
            return View("ConsultarResultados", resultados);
        }

        [HttpPost]
        public async Task<IActionResult> CompletarCita(int citaId)
        {
            var cita = await _citaService.GetByIdSaveViewModel(citaId);
            if (cita == null)
            {
                return NotFound();
            }

            cita.Estado = EstadoCita.Completada;
            await _citaService.Update(cita);

            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

    }
}
