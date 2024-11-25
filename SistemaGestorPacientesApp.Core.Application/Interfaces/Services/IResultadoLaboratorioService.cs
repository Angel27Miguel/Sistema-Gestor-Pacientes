using SistemaGestorPacientesApp.Core.Application.ViewModels.ResultadoLaboratorio;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Services
{
    public interface IResultadoLaboratorioService : IGenericService<SaveResultadoLaboratorioViewModel, ResultadoLaboratorioViewModel>
    {
        Task UpdateResultado(SaveResultadoLaboratorioViewModel vm);
        Task<List<ResultadoLaboratorioViewModel>> GetByCedula(string cedula);
        Task<List<ResultadoLaboratorioViewModel>> GetByCitaId(int citaId);
        
    }
}
