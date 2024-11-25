using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories
{
    public interface IPruebaLaboratorioRepository : IGenericRepository<PruebaLaboratorio>
    {
        Task<PruebaLaboratorio> GetByIdWithIncludeAsync(int id, List<string> list);
    }
}
