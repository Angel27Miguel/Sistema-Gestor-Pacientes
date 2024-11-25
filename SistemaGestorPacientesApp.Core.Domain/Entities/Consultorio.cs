using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class Consultorio : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Medico> Medicos { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }
        public ICollection<PruebaLaboratorio> PruebasLaboratorio { get; set; }
        public ICollection<Cita> Citas { get; set; }

    }
}
