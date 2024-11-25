using SistemaGestorPacientesApp.Core.Domain.Entities;


namespace SistemaGestorPacientesApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        public int? ConsultorioId { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
