using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente;
using SistemaGestorPacientesApp.Middlewares;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly IConsultorioService _consultorioService;
        private readonly ValidateUserSession _validateUserSession;

        public PacienteController(IPacienteService pacienteService, IConsultorioService consultorioService, ValidateUserSession validateUserSession)
        {
            _pacienteService = pacienteService;
            _consultorioService = consultorioService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _pacienteService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SavePacienteViewModel vm = new();
            vm.Consultorio = await _consultorioService.GetAllViewModel();
            return View("SavePaciente", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePacienteViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorio = await _consultorioService.GetAllViewModel();
                return View("SavePaciente", vm);
            }

            SavePacienteViewModel pacienteVm = await _pacienteService.Add(vm);

            if (pacienteVm.Id != 0 && pacienteVm != null)
            {
                pacienteVm.Foto = UploadFile(vm.File, pacienteVm.Id);
                await _pacienteService.Update(pacienteVm);
            }

            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePacienteViewModel vm = await _pacienteService.GetByIdSaveViewModel(id);
            vm.Consultorio = await _consultorioService.GetAllViewModel();
            return View("SavePaciente", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePacienteViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorio = await _consultorioService.GetAllViewModel();
                return View("SavePaciente", vm);
            }

            SavePacienteViewModel pacienteVm = await _pacienteService.GetByIdSaveViewModel(vm.Id);
            vm.Foto = UploadFile(vm.File, vm.Id, true, pacienteVm.Foto);
            await _pacienteService.Update(vm);
            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _pacienteService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _pacienteService.Delete(id);

            string basePath = $"/Images/Pacientes/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            string basePath = $"/Images/Pacientes/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
