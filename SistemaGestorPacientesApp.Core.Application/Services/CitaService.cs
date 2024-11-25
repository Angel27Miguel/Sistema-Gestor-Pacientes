using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Cita;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IConsultorioRepository _consultorioRepository;

    public CitaService(ICitaRepository citaRepository, IConsultorioRepository consultorioRepository)
    {
        _citaRepository = citaRepository;
        _consultorioRepository = consultorioRepository;
    }

    public async Task<List<CitaViewModel>> GetAllViewModel()
    {
        var citaList = await _citaRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "Medico", "Consultorio" });

        return citaList.Select(cita => new CitaViewModel
        {
            Id = cita.Id,
            NombrePaciente = cita.Paciente.Nombre,
            NombreMedico = cita.Medico.Nombre,
            FechaCita = cita.FechaCita,
            HoraCita = cita.HoraCita,
            Causa = cita.Causa,
            Estado = cita.Estado.ToString(),  
            ConsultorioId = cita.ConsultorioId,
        }).ToList();
    }

    public async Task<SaveCitaViewModel> Add(SaveCitaViewModel vm)
    {
        Cita cita = new()
        {
            PacienteId = vm.PacienteId,
            MedicoId = vm.MedicoId,
            ConsultorioId = vm.ConsultorioId,
            FechaCita = vm.FechaCita,
            HoraCita = vm.HoraCita,
            Causa = vm.Causa,
            Estado = vm.Estado 
        };

        cita = await _citaRepository.AddAsync(cita);

        return new SaveCitaViewModel
        {
            Id = cita.Id,
            PacienteId = cita.PacienteId,
            MedicoId = cita.MedicoId,
            ConsultorioId = cita.ConsultorioId,
            FechaCita = cita.FechaCita,
            HoraCita = cita.HoraCita,
            Causa = cita.Causa,
            Estado = cita.Estado
        };
    }

    public async Task Update(SaveCitaViewModel vm)
    {
        var cita = await _citaRepository.GetByIdAsync(vm.Id);

        if (cita == null)
            throw new Exception("Cita no encontrada");

        cita.PacienteId = vm.PacienteId;
        cita.MedicoId = vm.MedicoId;
        cita.ConsultorioId = vm.ConsultorioId;
        cita.FechaCita = vm.FechaCita;
        cita.HoraCita = vm.HoraCita;
        cita.Causa = vm.Causa;
        cita.Estado = vm.Estado;

        await _citaRepository.UpdateAsync(cita);
    }

    public async Task Delete(int id)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        if (cita == null)
            throw new Exception("Cita no encontrada");

        await _citaRepository.DeleteAsync(cita);
    }

    public async Task<SaveCitaViewModel> GetByIdSaveViewModel(int id)
    {
        var cita = await _citaRepository.GetCitaConDetallesAsync(id);

        if (cita == null)
            throw new Exception("Cita no encontrada");

        return new SaveCitaViewModel
        {
            Id = cita.Id,
            PacienteId = cita.PacienteId,
            NombrePaciente = cita.Paciente.Nombre, 
            MedicoId = cita.MedicoId,
            NombreMedico = cita.Medico.Nombre, 
            ConsultorioId = cita.ConsultorioId,
            NombreConsultorio = cita.Consultorio?.Nombre, 
            FechaCita = cita.FechaCita,
            HoraCita = cita.HoraCita,
            Causa = cita.Causa,
            Estado = cita.Estado
        };
    }
        public async Task UpdateEstado(SaveCitaViewModel vm)
        {
            var cita = await _citaRepository.GetByIdAsync(vm.Id);

            if (cita == null)
                throw new Exception("Cita no encontrada");

            cita.Estado = vm.Estado;

            await _citaRepository.UpdateAsync(cita);
        }

    }
}
