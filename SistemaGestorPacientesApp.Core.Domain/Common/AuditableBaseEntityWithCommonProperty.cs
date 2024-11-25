
namespace SistemaGestorPacientesApp.Core.Domain.Common
{
    public class AuditableBaseEntityWithCommonProperty : AuditableBaseEntity
    { 
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}