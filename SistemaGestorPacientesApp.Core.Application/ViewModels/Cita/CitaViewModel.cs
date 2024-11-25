namespace SistemaGestorPacientesApp.Core.Application.ViewModels.Cita
{
    public class CitaViewModel
    {
        public int Id { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Causa { get; set; }
        public string Estado { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int? ConsultorioId { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
    }
}
