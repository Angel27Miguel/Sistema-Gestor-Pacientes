using SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Medico;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente;
using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using System.ComponentModel.DataAnnotations;


namespace SistemaGestorPacientesApp.Core.Application.ViewModels.Cita
{
    public class SaveCitaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de la cita es requerida.")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "La hora de la cita es requerida.")]
        public TimeSpan HoraCita { get; set; }

        [Required(ErrorMessage = "La causa es requerida.")]
        public string Causa { get; set; }

        public EstadoCita Estado { get; set; }

        [Required(ErrorMessage = "El paciente es requerido.")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "El médico es requerido.")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "El consultorio es requerido.")]
        public int? ConsultorioId { get; set; }
        public string? NombrePaciente { get; set; }
        public string? NombreMedico { get; set; }
        public string? NombreConsultorio { get; set; }
        public List<PacienteViewModel>? Pacientes { get; set; } 
        public List<ConsultorioViewModel>? Consultorios { get; set; } 
        public List<MedicoViewModel>? Medicos { get; set; }
        public List<PruebaLaboratorioViewModel>? PruebasLaboratorio { get; set; }
        public List<int>? PruebasSeleccionadas { get; set; }
        public string? NombrePrueba { get;  set; }
        public string? ResultadoPrueba { get;  set; }
    }
}
