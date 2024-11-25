using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio
{
    public class ResultadoLaboratorioViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int CitaId { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string CedulaPaciente { get; set; }
        public int PruebaLaboratorioId { get; set; }
        public string NombrePrueba { get; set; }
        public bool EsCompletada { get; set; }
        public string? Resultado { get; set; }
    }
}
