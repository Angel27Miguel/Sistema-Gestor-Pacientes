using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Medico;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<SaveMedicoViewModel> Add(SaveMedicoViewModel vm)
        {
            Medico medico = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Correo = vm.Correo,
                Telefono = vm.Telefono,
                Cedula = vm.Cedula,
                Foto = vm.Foto,
                ConsultorioId = vm.ConsultorioId
            };

            medico = await _medicoRepository.AddAsync(medico);

            SaveMedicoViewModel medicoVm = new()
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Telefono = medico.Telefono,
                Cedula = medico.Cedula,
                Foto = medico.Foto,
                ConsultorioId = medico.ConsultorioId
            };

            return medicoVm;
        }

        public async Task Update(SaveMedicoViewModel vm)
        {
            Medico medico = await _medicoRepository.GetByIdAsync(vm.Id);
            medico.Nombre = vm.Nombre;
            medico.Apellido = vm.Apellido;
            medico.Correo = vm.Correo;
            medico.Telefono = vm.Telefono;
            medico.Cedula = vm.Cedula;
            medico.Foto = vm.Foto;
            medico.ConsultorioId = vm.ConsultorioId;

            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task Delete(int id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            await _medicoRepository.DeleteAsync(medico);
        }

        public async Task<SaveMedicoViewModel> GetByIdSaveViewModel(int id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);

            SaveMedicoViewModel vm = new()
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Telefono = medico.Telefono,
                Cedula = medico.Cedula,
                Foto = medico.Foto,
                ConsultorioId = medico.ConsultorioId
            };

            return vm;
        }

        public async Task<List<MedicoViewModel>> GetAllViewModel()
        {
            var medicoList = await _medicoRepository.GetAllWithIncludeAsync(new List<string> { "Consultorio" });

            return medicoList.Select(medico => new MedicoViewModel
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Telefono = medico.Telefono,
                Cedula = medico.Cedula,
                Foto = medico.Foto,
                ConsultorioId = medico.ConsultorioId,
                NombreConsultorio = medico.Consultorio.Nombre
            }).ToList();
        }
    }
}
