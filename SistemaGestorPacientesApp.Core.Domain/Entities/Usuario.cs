using SistemaGestorPacientesApp.Core.Domain.Common;

namespace SistemaGestorPacientesApp.Core.Domain.Entities
{
    public class Usuario : AuditableBaseEntityWithCommonProperty
    {
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string? Contraseña { get; set; }
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
