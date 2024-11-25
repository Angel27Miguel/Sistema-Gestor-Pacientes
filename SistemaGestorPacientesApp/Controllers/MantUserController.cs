using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.Services;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Middlewares;
using SistemaGestorPacientesApp.Models;
using System.Diagnostics;

namespace SistemaGestorPacientesApp.Controllers
{
    public class MantUserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<MantUserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ValidateUserSession _validateUserSession;

        public MantUserController(ILogger<MantUserController> logger, ValidateUserSession validateUserSession, IUserService userService, IUserRepository userRepository)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var usuarios = await _userService.GetAllViewModel();
            return View(usuarios);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveUser", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (await _userRepository.UserNameExistsAsync(vm.NombreUsuario))
            {
                vm.NombreUsuarioError = "El nombre de usuario ya existe. Por favor, elige otro.";
                return View("SaveUser", vm);
            }

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "MantUser", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveUser", await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            var user = await _userService.GetByIdSaveViewModel(vm.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Nombre = vm.Nombre;
            user.Apellido = vm.Apellido;
            user.NombreUsuario = vm.NombreUsuario;
            user.Correo = vm.Correo;
            user.EsAdministrador = vm.EsAdministrador;
            user.ConsultorioId = vm.ConsultorioId;

            // Si se ingreso una nueva contraseña se actualizara
            if (!string.IsNullOrEmpty(vm.Contraseña))
            {
                user.Contraseña = vm.Contraseña;
                user.ConfirmarContraseña = vm.ConfirmarContraseña;
            }

            await _userService.Update(user);
            return RedirectToRoute(new { controller = "MantUser", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _userService.Delete(id);
            return RedirectToRoute(new { controller = "MantUser", action = "Index" });
        }
    }
}
