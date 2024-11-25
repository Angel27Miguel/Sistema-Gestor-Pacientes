

using SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Services
{
    public interface IPacienteService : IGenericService<SavePacienteViewModel, PacienteViewModel>
    {
    }
}
