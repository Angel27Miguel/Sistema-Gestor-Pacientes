using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class Paciente : AuditableBaseEntityWithCommonProperty
    {
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsFumador { get; set; }
        public string Alergias { get; set; }
        public string? Foto { get; set; } 
        public int ConsultorioId { get; set; } 
        public virtual Consultorio Consultorio { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }

}
