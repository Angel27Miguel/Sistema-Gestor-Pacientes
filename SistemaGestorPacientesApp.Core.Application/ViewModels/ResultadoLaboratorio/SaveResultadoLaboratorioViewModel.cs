using SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio
{
    public class SaveResultadoLaboratorioViewModel
    {
        public int Id { get; set; }
        public int CitaId { get; set; }
        public bool EsCompletada { get; set; }
        public string? Resultado { get; set; }
        public int PacienteId { get; set; }
        public DateTime Fecha { get; set; }
        public IEnumerable<PruebaLaboratorioViewModel>? Pruebas { get; set; }
        public int PruebaLaboratorioId { get; set; }
        public string? NombrePrueba { get; set; }
    }
}
