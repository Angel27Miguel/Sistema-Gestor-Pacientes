using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Medico;
using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio;
using SistemaGestorPacientesApp.Middlewares;
using System.Threading.Tasks;

public class ResultadosLaboratorioController : Controller
{
    private readonly IResultadoLaboratorioService _resultadoLaboratorioService;
    private readonly ValidateUserSession _validateUserSession;

    public ResultadosLaboratorioController(IResultadoLaboratorioService resultadoLaboratorioService, ValidateUserSession validateUserSession)
    {
        _resultadoLaboratorioService = resultadoLaboratorioService;
        _validateUserSession = validateUserSession;
    }

    public async Task<IActionResult> Index()
    {
        if (!_validateUserSession.HasUser())
        {
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        var resultados = await _resultadoLaboratorioService.GetAllViewModel();

        return View(resultados);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!_validateUserSession.HasUser())
        {
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        return View("ReportarResultado", await _resultadoLaboratorioService.GetByIdSaveViewModel(id));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SaveResultadoLaboratorioViewModel vm)
    {
        if (!_validateUserSession.HasUser())
        {
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        if (!ModelState.IsValid)
        {
            return View("ReportarResultado", vm);
        }


        vm.EsCompletada = true;
        await _resultadoLaboratorioService.UpdateResultado(vm);
        return RedirectToRoute(new { controller = "ResultadosLaboratorio", action = "Index" });
    }

    [HttpPost]
    public async Task<IActionResult> SearchByCedula(string cedula)
    {
        if (!_validateUserSession.HasUser())
        {
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        var resultados = await _resultadoLaboratorioService.GetByCedula(cedula);
        return View("Index", resultados);
    }



}