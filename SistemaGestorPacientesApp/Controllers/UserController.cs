using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Middlewares;
using SistemaGestorPacientesApp.Core.Application.Helpers;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;

namespace SistemaGestorPacientesApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession, IUserRepository userRepository)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            UserViewModel userVm = await _userService.Login(vm);
            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrecto");
            }

            return View(vm);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (await _userRepository.UserNameExistsAsync(vm.NombreUsuario))
            {
                vm.NombreUsuarioError = "El nombre de usuario ya existe. Por favor, elige otro.";
                return View(vm); 
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }



    }
}
