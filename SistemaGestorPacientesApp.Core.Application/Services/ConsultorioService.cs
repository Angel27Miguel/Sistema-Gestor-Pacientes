using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly IConsultorioRepository _consultorioRepository;

        public ConsultorioService(IConsultorioRepository consultorioRepository)
        {
            _consultorioRepository = consultorioRepository;
        }

        public async Task<List<ConsultorioViewModel>> GetAllViewModel()
        {
            var consultorioList = await _consultorioRepository.GetAllAsync();

            return consultorioList.Select(consultorio => new ConsultorioViewModel
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            }).ToList();
        }

        public async Task<SaveConsultorioViewModel> Add(SaveConsultorioViewModel vm)
        {
            Consultorio consultorio = new()
            {
                Id=vm.Id,
                Nombre = vm.Nombre
            };

            consultorio = await _consultorioRepository.AddAsync(consultorio);

            return new SaveConsultorioViewModel
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };
        }

        public async Task Update(SaveConsultorioViewModel vm)
        {
            Consultorio consultorio = await _consultorioRepository.GetByIdAsync(vm.Id);
            consultorio.Nombre = vm.Nombre;

            await _consultorioRepository.UpdateAsync(consultorio);
        }

        public async Task Delete(int id)
        {
            var consultorio = await _consultorioRepository.GetByIdAsync(id);
            await _consultorioRepository.DeleteAsync(consultorio);
        }

        public async Task<SaveConsultorioViewModel> GetByIdSaveViewModel(int id)
        {
            var consultorio = await _consultorioRepository.GetByIdAsync(id);

            return new SaveConsultorioViewModel
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };
        }
    }
}
