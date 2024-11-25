using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class ResultadoLaboratorioService : IResultadoLaboratorioService
    {
        private readonly IResultadoLaboratorioRepository _resultadoRepository;

        public ResultadoLaboratorioService(IResultadoLaboratorioRepository resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<List<ResultadoLaboratorioViewModel>> GetAllViewModel()
        {
            var resultadoList = await _resultadoRepository.GetAllWithIncludeAsync(new List<string> { "Cita.Paciente", "Prueba" });

            return resultadoList
                .Where(resultado => !resultado.EsCompletada)
                .Select(resultado => new ResultadoLaboratorioViewModel
                {
                    Id = resultado.Id,
                    PacienteId = resultado.Cita.PacienteId,
                    NombrePaciente = resultado.Cita.Paciente.Nombre,
                    ApellidoPaciente = resultado.Cita.Paciente.Apellido,
                    CedulaPaciente = resultado.Cita.Paciente.Cedula,
                    PruebaLaboratorioId = resultado.PruebaLaboratorioId,
                    NombrePrueba = resultado.Prueba?.Nombre ?? "Sin nombre",
                    EsCompletada = resultado.EsCompletada,
                    Resultado = resultado.Resultado
                }).ToList();
        }


        public async Task<SaveResultadoLaboratorioViewModel> Add(SaveResultadoLaboratorioViewModel vm)
        {
            ResultadoLaboratorio resultado = new()
            {
                CitaId = vm.CitaId,
                EsCompletada = false, 
                Resultado = vm.Resultado,
                PruebaLaboratorioId = vm.PruebaLaboratorioId,
                PacienteId = vm.PacienteId,
                Fecha = DateTime.Now
            };

            resultado = await _resultadoRepository.AddAsync(resultado);

            return new SaveResultadoLaboratorioViewModel
            {
                Id = resultado.Id,
                CitaId = resultado.CitaId,
                EsCompletada = resultado.EsCompletada,
                Resultado = resultado.Resultado,
                PacienteId = resultado.PacienteId, 
                Fecha = DateTime.Now
                
            };
        }

        public async Task Update(SaveResultadoLaboratorioViewModel vm)
        {
            ResultadoLaboratorio resultado = await _resultadoRepository.GetByIdAsync(vm.Id);
            if (resultado != null)
            {
                resultado.CitaId = vm.CitaId;
                resultado.EsCompletada = vm.EsCompletada;
                resultado.Resultado = vm.Resultado;
                resultado.PacienteId= vm.PacienteId;
                resultado.PruebaLaboratorioId = vm.PruebaLaboratorioId;

                await _resultadoRepository.UpdateAsync(resultado);
            }
        }

        public async Task Delete(int id)
        {
            var resultado = await _resultadoRepository.GetByIdAsync(id);
            if (resultado != null)
            {
                await _resultadoRepository.DeleteAsync(resultado);
            }
        }

        public async Task<SaveResultadoLaboratorioViewModel> GetByIdSaveViewModel(int id)
        {
            var resultadoList = await _resultadoRepository.GetAllWithIncludeAsync(new List<string> { "Cita.Paciente" });
            var resultado = resultadoList.FirstOrDefault(r => r.Id == id);

            if (resultado == null) return new SaveResultadoLaboratorioViewModel(); 

            return new SaveResultadoLaboratorioViewModel
            {
                Id = resultado.Id,
                CitaId = resultado.CitaId,
                EsCompletada = resultado.EsCompletada,
                Resultado = resultado.Resultado,
                PacienteId = resultado.PacienteId,
                PruebaLaboratorioId = resultado.PruebaLaboratorioId,
                Fecha = resultado.Fecha
                
            };
        }



        public async Task<List<ResultadoLaboratorioViewModel>> GetByCedula(string cedula)
        {
            var resultadoList = await _resultadoRepository.GetAllWithIncludeAsync(new List<string> { "Cita.Paciente", "Prueba" });

            return resultadoList
                .Where(resultado => resultado.Cita.Paciente.Cedula == cedula)
                .Select(resultado => new ResultadoLaboratorioViewModel
                {
                    Id = resultado.Id,
                    PacienteId = resultado.Cita.PacienteId,
                    NombrePaciente = resultado.Cita.Paciente.Nombre,
                    ApellidoPaciente = resultado.Cita.Paciente.Apellido,
                    CedulaPaciente = resultado.Cita.Paciente.Cedula,
                    PruebaLaboratorioId = resultado.PruebaLaboratorioId,
                    NombrePrueba = resultado.Prueba?.Nombre ?? "Sin nombre",
                    EsCompletada = resultado.EsCompletada,
                    Resultado = resultado.Resultado
                }).ToList();
        }

        public async Task<List<ResultadoLaboratorioViewModel>> GetByCitaId(int citaId)
        {
            var resultadoList = await _resultadoRepository.GetAllWithIncludeAsync(new List<string> { "Cita.Paciente", "Prueba" });

            return resultadoList
                .Where(resultado => resultado.CitaId == citaId)
                .Select(resultado => new ResultadoLaboratorioViewModel
                {
                    NombrePrueba = resultado.Prueba.Nombre, 
                    EsCompletada = resultado.EsCompletada, 
                    CitaId = resultado.CitaId 
                }).ToList();
        }

        public async Task UpdateResultado(SaveResultadoLaboratorioViewModel vm)
        {
            ResultadoLaboratorio resultado = await _resultadoRepository.GetByIdAsync(vm.Id);
            if (resultado != null)
            {
                resultado.EsCompletada = vm.EsCompletada;
                resultado.Resultado = vm.Resultado;

                await _resultadoRepository.UpdateAsync(resultado);
            }
        }
    }
}
