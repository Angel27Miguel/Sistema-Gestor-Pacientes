using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class Medico : AuditableBaseEntityWithCommonProperty
    {
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string? Foto { get; set; }
        public int? ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}
