using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class ResultadoLaboratorio : AuditableBaseEntity
    {
        public DateTime Fecha { get; set; }
        public string? Resultado { get; set; }

        public int CitaId { get; set; }
        public Cita Cita { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public virtual PruebaLaboratorio Prueba { get; set; }
        public bool EsCompletada { get; set; }
        public int PruebaLaboratorioId { get; set; }


    }
}
