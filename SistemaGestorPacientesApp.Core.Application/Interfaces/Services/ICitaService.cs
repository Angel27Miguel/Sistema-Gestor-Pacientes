using SistemaGestorPacientesApp.Core.Application.ViewModels.Cita;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Services
{
    public interface ICitaService : IGenericService<SaveCitaViewModel, CitaViewModel>
    {
        Task UpdateEstado(SaveCitaViewModel vm);
    }
}
