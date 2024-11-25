using SistemaGestorPacientesApp.Core.Domain.Common;
public enum EstadoCita
{
    PendienteConsulta,
    PendienteResultados,
    Completada
}

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class Cita : AuditableBaseEntity
    {
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Causa { get; set; }
        public EstadoCita Estado { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int? ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}
