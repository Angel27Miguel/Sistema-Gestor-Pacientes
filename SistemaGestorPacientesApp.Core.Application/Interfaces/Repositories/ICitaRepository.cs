using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories
{
    public interface ICitaRepository : IGenericRepository<Cita>
    {
        Task<Cita> GetCitaConDetallesAsync(int id);
    }
}
