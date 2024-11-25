using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Medico;
using SistemaGestorPacientesApp.Middlewares;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoService _medicoService;
        private readonly IConsultorioService _consultorioService;
        private readonly ValidateUserSession _validateUserSession;

        public MedicoController(IMedicoService medicoService, IConsultorioService consultorioService, ValidateUserSession validateUserSession)
        {
            _medicoService = medicoService;
            _consultorioService = consultorioService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _medicoService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveMedicoViewModel vm = new();
            vm.Consultorio = await _consultorioService.GetAllViewModel();
            return View("SaveMedico", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorio = await _consultorioService.GetAllViewModel();
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicoVm = await _medicoService.Add(vm);

            if (medicoVm.Id != 0 && medicoVm != null)
            {
                medicoVm.Foto = UploadFile(vm.File, medicoVm.Id);
                await _medicoService.Update(medicoVm);
            }

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveMedicoViewModel vm = await _medicoService.GetByIdSaveViewModel(id);
            vm.Consultorio = await _consultorioService.GetAllViewModel();
            return View("SaveMedico", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Consultorio = await _consultorioService.GetAllViewModel();
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicoVm = await _medicoService.GetByIdSaveViewModel(vm.Id);
            vm.Foto = UploadFile(vm.File, vm.Id, true, medicoVm.Foto);
            await _medicoService.Update(vm);
            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _medicoService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _medicoService.Delete(id);

            string basePath = $"/Images/Medicos/{id}";
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

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            string basePath = $"/Images/Medicos/{id}";
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
