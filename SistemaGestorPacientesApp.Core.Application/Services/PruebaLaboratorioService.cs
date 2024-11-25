using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class PruebaLaboratorioService : IPruebaLaboratorioService
    {
        private readonly IPruebaLaboratorioRepository _pruebaLaboratorioRepository;

        public PruebaLaboratorioService(IPruebaLaboratorioRepository pruebaLaboratorioRepository)
        {
            _pruebaLaboratorioRepository = pruebaLaboratorioRepository;
        }

        public async Task<SavePruebaLaboratorioViewModel> Add(SavePruebaLaboratorioViewModel vm)
        {
            PruebaLaboratorio pruebaLaboratorio = new()
            {
                Nombre = vm.Nombre,
                ConsultorioId = vm.ConsultorioId
            };

            pruebaLaboratorio = await _pruebaLaboratorioRepository.AddAsync(pruebaLaboratorio);

            return new SavePruebaLaboratorioViewModel
            {
                Id = pruebaLaboratorio.Id,
                Nombre = pruebaLaboratorio.Nombre,
                ConsultorioId = pruebaLaboratorio.ConsultorioId
            };
        }

        public async Task Update(SavePruebaLaboratorioViewModel vm)
        {
            PruebaLaboratorio pruebaLaboratorio = await _pruebaLaboratorioRepository.GetByIdAsync(vm.Id);
            if (pruebaLaboratorio != null)
            {
                pruebaLaboratorio.Nombre = vm.Nombre;
                pruebaLaboratorio.ConsultorioId = vm.ConsultorioId;

                await _pruebaLaboratorioRepository.UpdateAsync(pruebaLaboratorio);
            }
        }

        public async Task Delete(int id)
        {
            var pruebaLaboratorio = await _pruebaLaboratorioRepository.GetByIdAsync(id);
            if (pruebaLaboratorio != null)
            {
                await _pruebaLaboratorioRepository.DeleteAsync(pruebaLaboratorio);
            }
        }

        public async Task<List<PruebaLaboratorioViewModel>> GetAllViewModel()
        {
            var pruebaLaboratorioList = await _pruebaLaboratorioRepository.GetAllWithIncludeAsync(new List<string> { "Consultorio" });

            return pruebaLaboratorioList.Select(prueba => new PruebaLaboratorioViewModel
            {
                Id = prueba.Id,
                Nombre = prueba.Nombre,
                ConsultorioId = prueba.ConsultorioId,
                NombreConsultorio = prueba.Consultorio.Nombre
            }).ToList();
        }

        public async Task<SavePruebaLaboratorioViewModel> GetByIdSaveViewModel(int id)
        {
            var pruebaLaboratorio = await _pruebaLaboratorioRepository.GetByIdAsync(id);

            return new SavePruebaLaboratorioViewModel
            {
                Id = pruebaLaboratorio.Id,
                Nombre = pruebaLaboratorio.Nombre,
                ConsultorioId = pruebaLaboratorio.ConsultorioId
                
            };
        }
    }
}
