using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class PruebaLaboratorio : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        public virtual ICollection<ResultadoLaboratorio> ResultadosLaboratorio { get; set; }

    }
}
