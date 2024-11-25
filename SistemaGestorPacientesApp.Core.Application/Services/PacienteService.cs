using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<SavePacienteViewModel> Add(SavePacienteViewModel vm)
        {
            Paciente paciente = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Cedula = vm.Cedula,
                FechaNacimiento = vm.FechaNacimiento,
                EsFumador = vm.EsFumador,
                Alergias = vm.Alergias,
                Foto = vm.Foto,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion,
                ConsultorioId = vm.ConsultorioId
            };

            paciente = await _pacienteRepository.AddAsync(paciente);

            SavePacienteViewModel pacienteVm = new()
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Cedula = paciente.Cedula,
                FechaNacimiento = paciente.FechaNacimiento,
                EsFumador = paciente.EsFumador,
                Alergias = paciente.Alergias,
                Foto = paciente.Foto,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                ConsultorioId = paciente.ConsultorioId
            };

            return pacienteVm;
        }

        public async Task Update(SavePacienteViewModel vm)
        {
            Paciente paciente = await _pacienteRepository.GetByIdAsync(vm.Id);
            paciente.Nombre = vm.Nombre;
            paciente.Apellido = vm.Apellido;
            paciente.Cedula = vm.Cedula;
            paciente.FechaNacimiento = vm.FechaNacimiento;
            paciente.EsFumador = vm.EsFumador;
            paciente.Alergias = vm.Alergias;
            paciente.Foto = vm.Foto;
            paciente.Telefono = vm.Telefono;
            paciente.Direccion = vm.Direccion;
            paciente.ConsultorioId = vm.ConsultorioId;

            await _pacienteRepository.UpdateAsync(paciente);
        }

        public async Task Delete(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            await _pacienteRepository.DeleteAsync(paciente);
        }

        public async Task<SavePacienteViewModel> GetByIdSaveViewModel(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            SavePacienteViewModel vm = new()
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Cedula = paciente.Cedula,
                FechaNacimiento = paciente.FechaNacimiento,
                EsFumador = paciente.EsFumador,
                Alergias = paciente.Alergias,
                Foto = paciente.Foto,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                ConsultorioId = paciente.ConsultorioId
            };

            return vm;
        }

        public async Task<List<PacienteViewModel>> GetAllViewModel()
        {
            var pacienteList = await _pacienteRepository.GetAllWithIncludeAsync(new List<string> { "Consultorio" });

            return pacienteList.Select(paciente => new PacienteViewModel
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Cedula = paciente.Cedula,
                FechaNacimiento = paciente.FechaNacimiento,
                EsFumador = paciente.EsFumador,
                Alergias = paciente.Alergias,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                Foto = paciente.Foto,
                ConsultorioId = paciente.ConsultorioId,
                NombreConsultorio = paciente.Consultorio.Nombre
            }).ToList();
        }
    }
}
