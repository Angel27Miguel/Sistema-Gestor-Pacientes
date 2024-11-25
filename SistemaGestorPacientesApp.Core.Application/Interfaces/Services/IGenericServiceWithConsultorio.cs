namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Services
{
    public interface IGenericServiceWithConsultorio<SaveViewModel, ViewModel> : IGenericService<SaveViewModel, ViewModel>
           where SaveViewModel : class
           where ViewModel : class
    {
    }
}
